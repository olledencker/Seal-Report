﻿//
// Copyright (c) Seal Report, Eric Pfirsch (sealreport@gmail.com), http://www.sealreport.org.
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. http://www.apache.org/licenses/LICENSE-2.0..
//
using Seal.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Seal.Model
{
    /// <summary>
    /// Base class for the Excel Converter
    /// </summary>
    public class SealExcelConverter : RootEditor
    {
        #region Editor

        protected override void UpdateEditorAttributes()
        {
            if (_dctd != null)
            {
                //Disable all properties
                foreach (var property in Properties) property.SetIsBrowsable(false);
                TypeDescriptor.Refresh(this);
            }
        }

        public override void InitEditor()
        {
            base.InitEditor();
        }

        #endregion

        /// <summary>
        /// Creates a basic SealExcelConverter
        /// </summary>
        public static SealExcelConverter Create(string assemblyDirectory)
        {
            SealExcelConverter result = null;
            //Check if an implementation is available in a .dll
            string applicationPath = string.IsNullOrEmpty(assemblyDirectory) ? Path.GetDirectoryName(Application.ExecutablePath) : assemblyDirectory;
            if (File.Exists(Path.Combine(applicationPath, "SealConverter.dll")))
            {
                try
                {
                    Assembly currentAssembly = AppDomain.CurrentDomain.Load("SealConverter");
                    Type t = currentAssembly.GetType("SealExcelConverter.ExcelConverter", true);
                    Object[] args = new Object[] { };
                    result = (SealExcelConverter)t.InvokeMember(null, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, args);
                    result.ApplicationPath = applicationPath;
                    //Load related DLLs
                    Assembly.LoadFrom(Path.Combine(applicationPath, "DocumentFormat.OpenXml.dll"));
                    Assembly.LoadFrom(Path.Combine(applicationPath, "EPPlus.dll"));
                }
                catch { }
            }

            if (result == null) result = new SealExcelConverter();
            
            return result;
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public override string ToString() {
            //PlaceHolder1
            return "Not implemented in the open source version. A commercial component is available at www.ariacom.com"; 
        }

        /// <summary>
        /// Current application path
        /// </summary>
        public string ApplicationPath = Path.GetDirectoryName(Application.ExecutablePath);

        /// <summary>
        /// Convert to Excel and save the result to a destination path
        /// </summary>
        public virtual string ConvertToExcel(string destination)
        {
            //PlaceHolder2
            throw new Exception("The Excel Converter is not implemented in the open source version...\r\nA commercial component is available at www.ariacom.com\r\n");
        }

        public virtual void SetConfigurations(List<string> configurations, ReportView view)
        {
        }

        public virtual List<string> GetConfigurations()
        {
            return new List<string>();
        }

        public virtual void ConfigureTemplateEditor(TemplateTextEditorForm frm, string propertyName, ref string template, ref string language) { }

        public IEntityHandler EntityHandler = null;

        public virtual string GetLicenseText()
        {
            return "";
        }

        public virtual void InitFromReferenceView(ReportView referenceView)
        {
        }
    }
}
