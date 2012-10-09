using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxHWebinar.ContextBusService;

namespace DxHWebinar
{
    public partial class EktronDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //1. Login to Ektron Connector through DxH
            ContextBusClient cbClient = new ContextBusClient();
            List<ConnectionParam> paramList = cbClient.LoadConnection("Ektron", "Ektron").ToList();
            cbClient.Login(paramList.ToArray(), "Ektron");

            //2. get "HtmlContent" ObjectDefinition
            FlyweightObjectDefinition flyweight = new FlyweightObjectDefinition() { Id = "HTMLContent" };
            FlyweightObjectDefinition[] flyweights = new FlyweightObjectDefinition[] { flyweight };
            ObjectDefinition contentObjectDef = cbClient.GetObjectDefinitionList(flyweights, "Ektron").FirstOrDefault();

            //3. Define criteria to retrieve content object instances from Ektron Connector
            ObjectInstanceCriteriaFilter criteria = new ObjectInstanceCriteriaFilter();
            List<CriteriaFilter> filters = new List<CriteriaFilter>();

            filters.Add( new CriteriaFilter(){
                FieldID = "FolderId",
                Operator = ObjectInstanceCriteriaFilterOperator.EqualTo,
                Value = 1
            });

            criteria.Filters = filters.ToArray();

            //4. Retrieve Object Instances and databind
            ObjectInstanceList objectInstanceReturn = cbClient.GetObjectInstanceList(contentObjectDef, criteria, "Ektron");

            lvObjects.DataSource = objectInstanceReturn.Results;
            lvObjects.DataBind();
        }
    }
}