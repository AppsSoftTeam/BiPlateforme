using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApp.Gnet_ReportingService;
using TestApp.ViewModels;

namespace TestApp.Utils
{
    public class SSRSConnection
    {
        public List<FoldersVM> GetListFolders()
        {

            ReportingService2010 rs = new ReportingService2010();
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Url = "http://win-dk4bgmj2cd2.globalnet.tn:80/ReportServer_GNET_DW/ReportService2010.asmx";
            CatalogItem[] items = rs.ListChildren("/", true);

            List<FoldersVM> Folders = new List<FoldersVM>();
            try
            {

                int i = 0;
                foreach (CatalogItem item in items)
                {
                    if (item.TypeName == "Folder")
                    {
                        Folders.Add(new FoldersVM
                        {
                            Id = i,
                            FolderName = item.Name,

                            Path = item.Path
                        });
                        i++;
                    }

                }
            }
            catch (Exception)
            {

            }
            return Folders;
        }
        public List<ReportSelectionVM> GetListReports(string FolderName)
        {

            ReportingService2010 rs = new ReportingService2010();
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Url = "http://win-dk4bgmj2cd2.globalnet.tn:80/ReportServer_GNET_DW/ReportService2010.asmx";
            CatalogItem[] items = rs.ListChildren("/" + FolderName, true);
            List<ReportSelectionVM> reports = new List<ReportSelectionVM>();
            List<ReportSelectionVM> Folders = new List<ReportSelectionVM>();
            try
            {

                int i = 0;
                foreach (CatalogItem item in items)
                {
                    if (item.TypeName == "Folder")
                    {
                        // Do something
                    }
                    reports.Add(new ReportSelectionVM
                    {
                        ReportId = i,
                        ReportName = item.Name,
                        //ReportCreationDate = item.CreationDate,
                        Path = item.Path
                    });
                    i++;
                }
            }
            catch (Exception)
            {

            }
            return reports;
        }
    }
}