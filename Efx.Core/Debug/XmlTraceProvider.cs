// Decompiled with JetBrains decompiler
// Type: Efx.Core.Debug.XmlTraceProvider
// Assembly: Efx.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0301a254350536a9
// MVID: 5F54FF60-2D98-4A85-8549-DF824E058455
// Assembly location: D:\Code\2DS\Aplicaciones\a\dll\.NET 4.0\Efx.Core.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace Efx.Core.Debug
{
  public sealed class XmlTraceProvider : ITraceProvider
  {
    private readonly string _path;

    public XmlTraceProvider()
    {
      string appSetting = ConfigurationManager.AppSettings["TraceStoragePath"];
      if (string.IsNullOrEmpty(appSetting))
        return;
      this.Enabled = true;
      this._path = Application.GetDirectoryPath(appSetting);
      if (Directory.Exists(this._path))
        return;
      Directory.CreateDirectory(this._path);
    }

    public void WriteLine(string pText, string pAdditionalInfo)
    {
      if (!this.Enabled)
        return;
      if (string.IsNullOrEmpty(pText))
        throw new ArgumentNullException(nameof (pText));
      string str1 = Guid.NewGuid().ToString();
      string str2 = DateTime.UtcNow.ToString("yyyy-MM-ddHHmmssZ", (IFormatProvider) CultureInfo.InvariantCulture);
      XmlTextWriter xmlTextWriter = new XmlTextWriter(Path.Combine(this._path, string.Format("error-{0}-{1}.xml", (object) str2, (object) str1)), Encoding.UTF8);
      try
      {
        xmlTextWriter.Formatting = Formatting.Indented;
        xmlTextWriter.WriteStartElement("error");
        xmlTextWriter.WriteAttributeString("errorId", str1);
        xmlTextWriter.WriteAttributeString("date", str2);
        xmlTextWriter.WriteStartElement("message");
        xmlTextWriter.WriteCData(pText);
        xmlTextWriter.WriteEndElement();
        xmlTextWriter.WriteStartElement("info");
        xmlTextWriter.WriteCData(pAdditionalInfo);
        xmlTextWriter.WriteEndElement();
        xmlTextWriter.WriteEndElement();
        xmlTextWriter.Flush();
      }
      finally
      {
        xmlTextWriter.Close();
      }
    }

    public int GetErrors(int pPageIndex, int pPageSize, IList<ErrorLogEntry> pErrorEntryList)
    {
      if (pPageIndex < 0)
        throw new ArgumentOutOfRangeException(nameof (pPageIndex));
      if (pPageSize < 0)
        throw new ArgumentOutOfRangeException(nameof (pPageSize));
      if (!this.Enabled)
        return 0;
      string path = this._path;
      FileSystemInfo[] files = (FileSystemInfo[]) new DirectoryInfo(path).GetFiles("error-*.xml");
      if (files.Length < 1)
        return 0;
      string[] pKeys = new string[files.Length];
      int num1 = 0;
      foreach (FileSystemInfo fileSystemInfo in files)
      {
        if (XmlTraceProvider.IsUserFile(fileSystemInfo.Attributes))
          pKeys[num1++] = Path.Combine(path, fileSystemInfo.Name);
      }
      InvariantStringArray.Sort(pKeys, 0, num1);
      Array.Reverse((Array) pKeys, 0, num1);
      if (pErrorEntryList != null)
      {
        int num2 = pPageIndex * pPageSize;
        int num3 = num2 + pPageSize < num1 ? num2 + pPageSize : num1;
        for (int index = num2; index < num3; ++index)
        {
          XmlTextReader xmlTextReader = new XmlTextReader(pKeys[index]);
          try
          {
            while (xmlTextReader.IsStartElement("error"))
              pErrorEntryList.Add(XmlTraceProvider.Decode((XmlReader) xmlTextReader));
          }
          finally
          {
            xmlTextReader.Close();
          }
        }
      }
      return num1;
    }

    public ErrorLogEntry GetError(string pID)
    {
      if (!this.Enabled)
        return new ErrorLogEntry();
      string[] files = Directory.GetFiles(this._path, string.Format("error-*-{0}.xml", (object) pID));
      if (files.Length < 1)
        throw new FileNotFoundException(string.Format("Cannot locate error file for error with ID {0}.", (object) pID));
      FileInfo fileInfo = new FileInfo(files[0]);
      ErrorLogEntry errorLogEntry = new ErrorLogEntry();
      if (!XmlTraceProvider.IsUserFile(fileInfo.Attributes))
        return errorLogEntry;
      XmlTextReader xmlTextReader = new XmlTextReader(fileInfo.FullName);
      try
      {
        return XmlTraceProvider.Decode((XmlReader) xmlTextReader);
      }
      finally
      {
        xmlTextReader.Close();
      }
    }

    public bool Enabled { get; private set; }

    private static ErrorLogEntry Decode(XmlReader pReader)
    {
      ErrorLogEntry errorLogEntry = new ErrorLogEntry() { Id = pReader.GetAttribute("errorId"), Date = DateTime.ParseExact(pReader.GetAttribute("date"), "yyyy-MM-ddHHmmssZ", (IFormatProvider) CultureInfo.InvariantCulture) };
      while (pReader.Read())
      {
        switch (pReader.LocalName)
        {
          case "message":
            int content1 = (int) pReader.MoveToContent();
            errorLogEntry.Message = pReader.ReadString();
            pReader.ReadEndElement();
            continue;
          case "info":
            int content2 = (int) pReader.MoveToContent();
            errorLogEntry.AdditionalInfo = pReader.ReadString();
            pReader.ReadEndElement();
            continue;
          default:
            continue;
        }
      }
      return errorLogEntry;
    }

    private static bool IsUserFile(FileAttributes pAttributes)
    {
      return (FileAttributes) 0 == (pAttributes & (FileAttributes.Hidden | FileAttributes.System | FileAttributes.Directory));
    }
  }
}
