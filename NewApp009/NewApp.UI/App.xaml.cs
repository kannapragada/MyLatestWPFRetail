using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Globalization;
using System.Threading;
using System.Collections;
using System.Windows.Markup;
using NewApp.BusinessTier.Common;
using NewApp.BusinessTier.Models;
using NewApp.Themes;
using NewApp.GenderTypeFactorySvc;
using NewApp.IDProofTypeFactorySvc;



namespace NewApp.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IDisposable
    {
        //amounttype
        private bool disposed;

        ~App()
    {
        this.Dispose(false);
    }

        public void Dispose()
        {
            if (!this.disposed)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
                this.disposed = true;
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Dispose();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Set the current culture to Dutch (Netherlands).
            Thread.CurrentThread.Name = "MainThread";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-IN");

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
            XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
    }
}
