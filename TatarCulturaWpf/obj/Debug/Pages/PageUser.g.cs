﻿#pragma checksum "..\..\..\Pages\PageUser.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8A1C747289E13A47B9163CBC1EEE2FD18A2015D17B7DD22559AF032E2A3281B2"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TatarCulturaWpf.Pages;


namespace TatarCulturaWpf.Pages {
    
    
    /// <summary>
    /// PageUser
    /// </summary>
    public partial class PageUser : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\Pages\PageUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBack;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Pages\PageUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAccount;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Pages\PageUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnComment;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\Pages\PageUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnKey;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Pages\PageUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBoxComment;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\Pages\PageUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBoxKey;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TatarCulturaWpf;component/pages/pageuser.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\PageUser.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btnBack = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\Pages\PageUser.xaml"
            this.btnBack.Click += new System.Windows.RoutedEventHandler(this.btnBackClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnAccount = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\Pages\PageUser.xaml"
            this.btnAccount.Click += new System.Windows.RoutedEventHandler(this.btnAccountClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnComment = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\Pages\PageUser.xaml"
            this.btnComment.Click += new System.Windows.RoutedEventHandler(this.btnCommentClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnKey = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\Pages\PageUser.xaml"
            this.btnKey.Click += new System.Windows.RoutedEventHandler(this.btnKeyClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ListBoxComment = ((System.Windows.Controls.ListBox)(target));
            return;
            case 6:
            this.ListBoxKey = ((System.Windows.Controls.ListBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

