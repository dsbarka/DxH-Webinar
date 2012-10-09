using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxHWebinar.ContextBusService;

namespace DxHWebinar
{
    public partial class AdventureWorks : System.Web.UI.Page
    {

        const string AdvWorksConnectorId = "AdventureWorks";

        protected void Page_Load(object sender, EventArgs e)
        {

            ContextBusClient cbClient = new ContextBusClient();
            ConnectionParam[] parameters = cbClient.LoadConnection("Advworks", AdvWorksConnectorId);

            cbClient.Login(parameters, AdvWorksConnectorId);

            FlyweightObjectDefinition[] flyweights = cbClient.GetObjectDefinitionNameList(AdvWorksConnectorId);
            lvObjects.DataSource = flyweights;
            lvObjects.DataBind();

            List<ObjectDefinition> objDefList = cbClient.GetObjectDefinitionList(flyweights, AdvWorksConnectorId).ToList();

            ObjectInstanceCriteriaFilter criteria = new ObjectInstanceCriteriaFilter();
            ObjectInstanceList objectInstanceReturn = cbClient.GetObjectInstanceList(objDefList.First(), criteria, AdvWorksConnectorId);

            lvObjectInstances.DataSource = objectInstanceReturn.Results;
            lvObjectInstances.DataBind();

            //Lets map a sharepoint object to AdventureWorks

            //Get Sharepoint ObjectDefinition
            FlyweightObjectDefinition flyweight = new FlyweightObjectDefinition() { Id = "SP|List|/sites/SiteCollection1|Team"};
            ObjectDefinition sharepointObjDef= cbClient.GetObjectDefinitionList(new FlyweightObjectDefinition[] { flyweight }, "SharePoint").First();

            //Get Employee definition from adventure works:
            ObjectDefinition employeeObjDef = objDefList.Find(d => d.DisplayName == "Employee");

            //build the workflow.
            TaskManagerClient taskClient = new TaskManagerClient();

            //1. Create login task
            LoginTask login = taskClient.CreateLoginTask(AdvWorksConnectorId, "");

            //2. Create Mapping Task
            MappingTask map = taskClient.CreateMappingTask(sharepointObjDef, employeeObjDef);

            MappableField field = map.TargetFields.SingleOrDefault(f => f.Id == "BusinessEntityID");
            field.Mapping.MappingType = MappingType.ConstMapping;
            field.Mapping.Value = 1;

            field = map.TargetFields.SingleOrDefault(f => f.Id == "PersonType");
            field.Mapping.MappingType = MappingType.ConstMapping;
            field.Mapping.Value = "EM";

            field = map.TargetFields.SingleOrDefault(f => f.Id == "FirstName");
            field.Mapping.MappingType = MappingType.FieldMapping;
            field.Mapping.Value = map.SourceObject.Fields.SingleOrDefault(f => f.Id == "First_x0020_Name");

            field = map.TargetFields.SingleOrDefault(f => f.Id == "LastName");
            field.Mapping.MappingType = MappingType.FieldMapping;
            field.Mapping.Value = map.SourceObject.Fields.SingleOrDefault(f => f.Id == "last_x0020_name");

            field = map.TargetFields.SingleOrDefault(f => f.Id == "JobTitle");
            field.Mapping.MappingType = MappingType.FieldMapping;
            field.Mapping.Value = map.SourceObject.Fields.SingleOrDefault(f => f.Id == "Title");

            //3. Save Task
            SaveObjectInstanceTask saveSharepointToEktron = taskClient.CreateSaveObjectTask(AdvWorksConnectorId);
            
            //4. Logout Task
            LogoutTask logout = taskClient.CreateLogoutTask(AdvWorksConnectorId);

        }

       
    }
}