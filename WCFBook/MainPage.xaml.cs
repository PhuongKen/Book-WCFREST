using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WCFBook.ServiceReference1;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WCFBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        BookServiceClient webService = new BookServiceClient();
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }
        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            getBook();
        }
        async void getBook()
        {
            try
            {
                ProgressBar.IsIndeterminate = true;
                ProgressBar.Visibility = Visibility.Visible;
                GridViewBook.ItemsSource = await webService.GetBookListAsync();
                ProgressBar.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
            }
            catch (Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
                ProgressBar.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
            }
        }
        private async void GridViewBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems.Count != 0)
                {
                    Book selectedBook = e.AddedItems[0] as Book;
                    TextBoxId.Text = selectedBook.BookId.ToString();
                    TextBoxTitle.Text = selectedBook.Title;
                    TextBoxISBN.Text = selectedBook.ISBN;
                }
            }
            catch
            {
                MessageDialog messageDialog = new MessageDialog("Error data!");
                await messageDialog.ShowAsync();
                ProgressBar.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
            }
        }
        private async void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProgressBar.IsIndeterminate = true;
                ProgressBar.Visibility = Visibility.Visible;
                Book newBook = new Book();
                newBook.BookId = Int32.Parse(TextBoxId.Text);
                newBook.Title = TextBoxTitle.Text;
                newBook.ISBN = TextBoxISBN.Text;
                string result = await webService.AddBookAsync(newBook);
                ProgressBar.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
                if (result != null)
                {
                    MessageDialog messageDialog = new MessageDialog("Inserted successfully!");
                    await messageDialog.ShowAsync();
                    Reset();
                }
                else
                {
                    MessageDialog messageDialog = new MessageDialog("Can't Insert!");
                    await messageDialog.ShowAsync();
                }
                getBook();
            }
            catch (Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
                ProgressBar.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
            }
        }
        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (GridViewBook.SelectedItem != null)
            {
                try
                {
                    ProgressBar.IsIndeterminate = true;
                    ProgressBar.Visibility = Visibility.Visible;
                    String id = ((GridViewBook.SelectedItem as Book).BookId).ToString();
                    string result = await webService.DeleteBookAsync(id);
                    if (result != null)
                    {
                        MessageDialog messageDialog = new MessageDialog("Delete successfully!");
                        await messageDialog.ShowAsync();
                        Reset();
                    }
                    else
                    {
                        MessageDialog messageDialog = new MessageDialog("Can't delete!");
                        await messageDialog.ShowAsync();
                    }
                    getBook();
                }
                catch (Exception ex)
                {
                    MessageDialog messageDialog = new MessageDialog(ex.Message);
                    await messageDialog.ShowAsync();
                    ProgressBar.Visibility = Visibility.Collapsed;
                    ProgressBar.IsIndeterminate = false;
                }
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog("Choise record to delete!");
                await messageDialog.ShowAsync();
            }
        }
        void Reset()
        {
            TextBoxId.Text = string.Empty;
            TextBoxTitle.Text = string.Empty;
            TextBoxISBN.Text = string.Empty;
        }
        private async void ButtonModify_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProgressBar.IsIndeterminate = true;
                ProgressBar.Visibility = Visibility.Visible;
                
                Book newBook = new Book();
                newBook.BookId = (GridViewBook.SelectedItem as Book).BookId;
                newBook.Title = TextBoxTitle.Text;
                newBook.ISBN = TextBoxISBN.Text;

                string result = await webService.UpdateBookAsync(newBook);
                ProgressBar.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
                if (result != null)
                {
                    MessageDialog messageDialog = new MessageDialog("Edited successfully!");
                    await messageDialog.ShowAsync();
                    Reset();
                }
                else
                {
                    MessageDialog messageDialog = new MessageDialog("Can't delete!");
                    await messageDialog.ShowAsync();
                }
                getBook();
            }
            catch
            {
                MessageDialog messageDialog = new MessageDialog("Choise Employee!");
                await messageDialog.ShowAsync();
                ProgressBar.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
            }
        }
    }
}
