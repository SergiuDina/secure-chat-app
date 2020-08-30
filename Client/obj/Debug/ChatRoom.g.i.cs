﻿#pragma checksum "..\..\ChatRoom.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "43E9776CE05DAC06CB8EEBAA43862AE57693CCAF4693F27B43BE68840995449D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Client;
using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
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


namespace Client {
    
    
    /// <summary>
    /// Window1
    /// </summary>
    public partial class Window1 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\ChatRoom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox messageTextBox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\ChatRoom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox clientsListBox;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\ChatRoom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox messagesListBox;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\ChatRoom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label usernameLabel;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\ChatRoom.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sendButton;
        
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
            System.Uri resourceLocater = new System.Uri("/Client;component/chatroom.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ChatRoom.xaml"
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
            this.messageTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 35 "..\..\ChatRoom.xaml"
            this.messageTextBox.IsMouseDirectlyOverChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.messageTextBox_IsMouseDirectlyOverChanged);
            
            #line default
            #line hidden
            
            #line 36 "..\..\ChatRoom.xaml"
            this.messageTextBox.KeyUp += new System.Windows.Input.KeyEventHandler(this.messageTextBox_KeyUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.clientsListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 3:
            this.messagesListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 4:
            this.usernameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.sendButton = ((System.Windows.Controls.Button)(target));
            
            #line 113 "..\..\ChatRoom.xaml"
            this.sendButton.Click += new System.Windows.RoutedEventHandler(this.sendButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
