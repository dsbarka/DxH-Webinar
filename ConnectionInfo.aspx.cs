using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DxHWebinar.ContextBusService;

namespace DxHWebinar
{
    public partial class ConnectionInfo : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            ContextBusClient cbClient = new ContextBusClient();
            ConnectionParam[] parameters = cbClient.GetConnectionParameterList("SharePoint");

            lvParams.DataSource = parameters;
            lvParams.DataBind();

        }
    }
}