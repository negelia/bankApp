﻿#pragma checksum "..\..\buh.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "031CB0A9EA23519AA25AB6BA97FBBF1F9394B5546C2318D1DE83F45D499F28C0"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WpfApp2;


namespace WpfApp2 {
    
    
    /// <summary>
    /// buh
    /// </summary>
    public partial class buh : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\buh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataBuh;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\buh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox buhTitle;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\buh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label errorTitle;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\buh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PlanCB;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\buh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label errorPlan;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\buh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CreditCB;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\buh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label errorCredit;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\buh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dateBuh;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\buh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label errorDate;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApp2;component/buh.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\buh.xaml"
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
            
            #line 8 "..\..\buh.xaml"
            ((WpfApp2.buh)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dataBuh = ((System.Windows.Controls.DataGrid)(target));
            
            #line 10 "..\..\buh.xaml"
            this.dataBuh.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dataBuh_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.buhTitle = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.errorTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.PlanCB = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.errorPlan = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.CreditCB = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.errorCredit = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.dateBuh = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 10:
            this.errorDate = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            
            #line 25 "..\..\buh.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.add);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 26 "..\..\buh.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.update);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 27 "..\..\buh.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.delete);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 30 "..\..\buh.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.export);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 31 "..\..\buh.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.exit);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

