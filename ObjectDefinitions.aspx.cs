using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DxHWebinar.ContextBusService;

namespace DxHWebinar
{
    public partial class ObjectDefinitions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ContextBusClient cbClient = new ContextBusClient();

            List<ConnectionParam> paramList = new List<ConnectionParam>();
            paramList.Add(new ConnectionParam() { Id = "siteurl", Value = "http://brian3:90/sites/SiteCollection1" });
            paramList.Add(new ConnectionParam() { Id = "domain", Value = "" });
            paramList.Add(new ConnectionParam() { Id = "username", Value = "Administrator" });
            paramList.Add(new ConnectionParam() { Id = "password", Value = "" });

            paramList = cbClient.LoadConnection("HubSpot - Adam", "HubSpot").ToList();

            cbClient.Login(paramList.ToArray(), "HubSpot");

            FlyweightObjectDefinition[] flyweights = cbClient.GetObjectDefinitionNameList("HubSpot");

            lvObjects.DataSource = flyweights;
            lvObjects.DataBind();
           

        }
    }
}