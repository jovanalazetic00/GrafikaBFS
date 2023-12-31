﻿#pragma checksum "..\..\DrawEllipse.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "46CAE7CF2D70039B35A779BC8C6256DB5AA65632706C41324AAC17492582AA59"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Project;
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
using Xceed.Wpf.Toolkit;


namespace Project {
    
    
    /// <summary>
    /// DrawEllipse
    /// </summary>
    public partial class DrawEllipseWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 41 "..\..\DrawEllipse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox radiusY;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\DrawEllipse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox radiusX;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\DrawEllipse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox strokeThickness;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\DrawEllipse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.ColorPicker strokeColor;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\DrawEllipse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.ColorPicker fillColor;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\DrawEllipse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\DrawEllipse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.ColorPicker textColor;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\DrawEllipse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ellipseIsTransparent;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\DrawEllipse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button drawEllipseButton;
        
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
            System.Uri resourceLocater = new System.Uri("/Project;component/drawellipse.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DrawEllipse.xaml"
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
            this.radiusY = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.radiusX = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.strokeThickness = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.strokeColor = ((Xceed.Wpf.Toolkit.ColorPicker)(target));
            return;
            case 5:
            this.fillColor = ((Xceed.Wpf.Toolkit.ColorPicker)(target));
            return;
            case 6:
            this.textBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.textColor = ((Xceed.Wpf.Toolkit.ColorPicker)(target));
            return;
            case 8:
            this.ellipseIsTransparent = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 9:
            this.drawEllipseButton = ((System.Windows.Controls.Button)(target));
            
            #line 122 "..\..\DrawEllipse.xaml"
            this.drawEllipseButton.Click += new System.Windows.RoutedEventHandler(this.drawEllipseButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 130 "..\..\DrawEllipse.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Cancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

