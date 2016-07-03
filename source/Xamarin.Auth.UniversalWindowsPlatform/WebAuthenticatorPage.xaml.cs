using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Xamarin.Auth
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WebAuthenticatorPage : Page
    {
        WebAuthenticator _auth;
        public WebAuthenticatorPage()
        {
            this.InitializeComponent();
            this.browser.LoadCompleted += browser_LoadCompleted;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _auth = (OAuth2Authenticator)e.Parameter;

            _auth.Completed += auth_Completed;

            Uri uri = await _auth.GetInitialUrlAsync();
            this.browser.Source = uri;
            browser.Navigate(uri);

            _auth.OnPageLoading(uri);
            _auth.OnPageLoaded(uri);

            base.OnNavigatedTo(e);
        }

        private void auth_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            try
            {
                //try to manually go back, might not work based on navigation method
                if (this.Frame.CanGoBack)
                    this.Frame.GoBack();
            }
            catch { }
        }

        void browser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            _auth.OnPageLoaded(e.Uri);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();

            return;
        }
    }
}
