﻿#pragma checksum "..\..\..\Pages\PageObject.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0878CF3F3BD5820126DA92809EBB85FD9CEAA23C96ECB7AF8A4AC3740850138B"
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
using Microsoft.Maps.MapControl.WPF;
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
    /// PageObject
    /// </summary>
    public partial class PageObject : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\Pages\PageObject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image UsersPhoto;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Pages\PageObject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelAttention;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Pages\PageObject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnComment;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Pages\PageObject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Maps.MapControl.WPF.Map MapZel;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\Pages\PageObject.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBoxComment;
        
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
            System.Uri resourceLocater = new System.Uri("/TatarCulturaWpf;component/pages/pageobject.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\PageObject.xaml"
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
            
            #line 11 "..\..\..\Pages\PageObject.xaml"
            ((TatarCulturaWpf.Pages.PageObject)(target)).IsVisibleChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.PageIsVisibleChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.UsersPhoto = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.labelAttention = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.BtnComment = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\Pages\PageObject.xaml"
            this.BtnComment.Click += new System.Windows.RoutedEventHandler(this.BtnCommentClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.MapZel = ((Microsoft.Maps.MapControl.WPF.Map)(target));
            
            #line 58 "..\..\..\Pages\PageObject.xaml"
            this.MapZel.MouseMove += new System.Windows.Input.MouseEventHandler(this.MapZelMouseMove);
            
            #line default
            #line hidden
            
            #line 58 "..\..\..\Pages\PageObject.xaml"
            this.MapZel.MouseLeave += new System.Windows.Input.MouseEventHandler(this.MapZelMouseLeave);
            
            #line default
            #line hidden
            
            #line 58 "..\..\..\Pages\PageObject.xaml"
            this.MapZel.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.MapZelMouseUp);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 72 "..\..\..\Pages\PageObject.xaml"
            ((MaterialDesignThemes.Wpf.PackIcon)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.PackIconMouseUp);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ListBoxComment = ((System.Windows.Controls.ListBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

