// Updated by XamlIntelliSenseFileGenerator 05.02.2022 17:25:26
#pragma checksum "..\..\marketAdmin.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "90C218D58DCA0FD855A6C90EAA0783EC1E7303F7FCB2DE0AE23D28ACF8093047"
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


namespace WpfApp2
{


    /// <summary>
    /// marketAdmin
    /// </summary>
    public partial class marketAdmin : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp2;component/marketadmin.xaml", System.UriKind.Relative);

#line 1 "..\..\marketAdmin.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.DataGrid data;
        internal System.Windows.Controls.TextBox fam;
        internal System.Windows.Controls.Label errorFam;
        internal System.Windows.Controls.TextBox im;
        internal System.Windows.Controls.Label errorName;
        internal System.Windows.Controls.TextBox otch;
        internal System.Windows.Controls.Label errorOtch;
        internal System.Windows.Controls.TextBox login;
        internal System.Windows.Controls.Label errorLogin;
        internal System.Windows.Controls.TextBox password;
        internal System.Windows.Controls.Label errorPass;
        internal System.Windows.Controls.ComboBox jobCB;
        internal System.Windows.Controls.Label errorJob;
        internal System.Windows.Controls.DatePicker birthday;
        internal System.Windows.Controls.Label errorDate;
    }
}

