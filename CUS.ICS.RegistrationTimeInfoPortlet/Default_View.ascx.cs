using System;
using System.Collections;   
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace CUS.ICS.RegistrationTimeInfo
{
    public partial class Default_View : Jenzabar.Portal.Framework.Web.UI.PortletViewBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Jenzabar.Portal.Framework.PortalUser.Current.HostID != null)
            {
                //**************************************************
                // if the logged in user has an ID, check for Time
                //**************************************************

                System.Data.SqlClient.SqlCommand sqlcmdSelectRegistrationTime = new System.Data.SqlClient.SqlCommand(
                    "SELECT ADD_BEG_DTE FROM TW_TIME_CTRL WHERE"
                    + " YR_CDE = (SELECT CURR_YR FROM CUST_INTRFC_CNTRL WHERE INTRFC_TYPE = 'REGTIME_PORTLET') AND TRM_CDE = (SELECT CURR_TRM FROM CUST_INTRFC_CNTRL WHERE INTRFC_TYPE = 'REGTIME_PORTLET') AND"
                    + " TEL_WEB_GRP_CDE = (SELECT TEL_WEB_GRP_CDE FROM STUDENT_MASTER WHERE ID_NUM = '" + Jenzabar.Portal.Framework.PortalUser.Current.HostID + "')",
                    new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["JenzabarConnectionString"].ConnectionString));

                try
                {
                    sqlcmdSelectRegistrationTime.Connection.Open();
                    System.Data.SqlClient.SqlDataReader sqlReader = sqlcmdSelectRegistrationTime.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        sqlReader.Read();
                        lblMsg.Text = "<b>" + (Convert.ToDateTime(sqlReader["ADD_BEG_DTE"].ToString())).ToString("dddd, MMMM dd @ h:mm tt") + "</b>.";
                        sqlReader.Close();
                        sqlcmdSelectRegistrationTime.Connection.Close();
                    }
                    else
                    {
                        this.ParentPortlet.ShowFeedback(Jenzabar.Portal.Framework.Web.UI.FeedbackType.Message, "Your registration time has not been set. Contact the Office of Registration & Records (830-372-8040) for more details.");
                    }
                    
                    System.Data.SqlClient.SqlCommand sqlcmdSelectRegEndTime = new System.Data.SqlClient.SqlCommand("SELECT last_drop_add_dte FROM year_term_table WHERE YR_CDE = (SELECT CURR_YR FROM CUST_INTRFC_CNTRL WHERE INTRFC_TYPE = 'REGTIME_PORTLET') AND TRM_CDE = (SELECT CURR_TRM FROM CUST_INTRFC_CNTRL WHERE INTRFC_TYPE = 'REGTIME_PORTLET')", new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["JenzabarConnectionString"].ConnectionString));
                    sqlcmdSelectRegEndTime.Connection.Open();
                    sqlReader = sqlcmdSelectRegEndTime.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        sqlReader.Read();
                        lblMessage.Text = "You can register at the time below or any time after through the last day to register, <b>" + (Convert.ToDateTime(sqlReader["LAST_DROP_ADD_DTE"].ToString())).ToString("dddd, MMMM dd @ h:mm tt") + "</b>.";
                    }
                }
                catch (Exception critical)
                {
                    lblError.ErrorMessage = "Error: Registration time(s) unavailable. <BR />" + critical.GetBaseException().Message;
                    lblError.Visible = true;
                }
                finally
                {
                    if (sqlcmdSelectRegistrationTime.Connection != null && sqlcmdSelectRegistrationTime.Connection.State == ConnectionState.Open)
                    {
                        sqlcmdSelectRegistrationTime.Connection.Close();
                    }
                }
            }
            else
            {
                ParentPortlet.ShowFeedbackGlobalized(Jenzabar.Portal.Framework.Web.UI.FeedbackType.Error, "MSG_NO_HOST_ID");
            }
            //lblMessage.Text = "If you need to make an change to your existing Spring schedule, the add/drop period will open beginning Monday, January 11th, at 8:00am, and will remain open until Friday, January 15th, at 5:00pm. If you did not preregister for your spring courses, you will need to register in-person in the Office of Registration and Records on the first floor of the Beck Center.";
        }
    }
}   