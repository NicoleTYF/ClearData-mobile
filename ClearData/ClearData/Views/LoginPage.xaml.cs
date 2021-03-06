﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearData.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClearData.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage 
    {
        public LoginPage() 
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
            this.usernameEntry.ReturnCommand = new Command(() => this.pswdEntry.Focus());
        }
    }
}

