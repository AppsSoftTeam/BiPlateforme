using System;
using System.Collections.Generic;
using System.DirectoryServices;
using TestApp.ViewModels;

namespace TestApp.Utils
{
    public class LDAPconnection
    {
        public List<UserSelectionVM> GetListUsers()
        {

            List<UserSelectionVM> lstADUsers = new List<UserSelectionVM>();
            string DomainPath = "LDAP://172.16.110.24/DC=globalnet,DC=tn";
            
            DirectoryEntry searchRoot = new DirectoryEntry(DomainPath);
            DirectorySearcher search = new DirectorySearcher(searchRoot);
            search.Filter = "(&(objectClass=user)(objectCategory=person))";
            search.PropertiesToLoad.Add("samaccountname");
            search.PropertiesToLoad.Add("usergroup");
            search.PropertiesToLoad.Add("displayname");//first name
            SearchResult result;
            try
            {
                SearchResultCollection resultCol = search.FindAll();
                if (resultCol != null)
                {
                    int i = 0;
                    for (int counter = 0; counter < resultCol.Count; counter++)
                    {
                        result = resultCol[counter];
                        if (result.Properties.Contains("samaccountname") &&
                            result.Properties.Contains("displayname"))
                        {
                            UserSelectionVM objSurveyUsers = new UserSelectionVM();
                            objSurveyUsers.UserId = i;
                            objSurveyUsers.UserName = (String)result.Properties["samaccountname"][0];
                            objSurveyUsers.DisplayName = (String)result.Properties["displayname"][0];
                            lstADUsers.Add(objSurveyUsers);
                        }
                        i++;
                    }
                }
            }

            catch (Exception ex)
            {

            }
            return lstADUsers;
        }


    }
}