namespace TypicalDXeXpressAppProject_DoSo.Win {
    partial class TypicalDXeXpressAppProject_DoSoWindowsFormsApplication {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule();
            this.module3 = new TypicalDXeXpressAppProject_DoSo.Module.TypicalDXeXpressAppProject_DoSoModule();
            this.module4 = new TypicalDXeXpressAppProject_DoSo.Module.Win.TypicalDXeXpressAppProject_DoSoWindowsFormsModule();
            this.conditionalAppearanceModule = new DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TypicalDXeXpressAppProject_DoSoWindowsFormsApplication
            // 
            this.ApplicationName = "TypicalDXeXpressAppProject_DoSo";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.module4);
            this.Modules.Add(this.conditionalAppearanceModule);
            this.UseOldTemplates = false;
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.TypicalDXeXpressAppProject_DoSoWindowsFormsApplication_DatabaseVersionMismatch);
            this.CustomizeLanguagesList += new System.EventHandler<DevExpress.ExpressApp.CustomizeLanguagesListEventArgs>(this.TypicalDXeXpressAppProject_DoSoWindowsFormsApplication_CustomizeLanguagesList);

            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule module2;
        private TypicalDXeXpressAppProject_DoSo.Module.TypicalDXeXpressAppProject_DoSoModule module3;
        private TypicalDXeXpressAppProject_DoSo.Module.Win.TypicalDXeXpressAppProject_DoSoWindowsFormsModule module4;
        private DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule conditionalAppearanceModule;
    }
}
