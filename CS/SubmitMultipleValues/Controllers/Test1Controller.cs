using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using SubmitMultipleValues.Models;
using System.Web.Script.Serialization;

namespace SubmitMultipleValues.Controllers
{
    public class Test1Controller : Controller
    {
        
        public ActionResult Index()
        {
            return View(new MainModel());
        }
      
        public ActionResult GridViewPartial()
        {
          
            return PartialView(BatchEditRepository.GridData);
        }
        public ActionResult GridViewCustomActionUpdate(string mainModel)
        {
            ViewData["SuccessFlag"] = UpdateAllValues(null, mainModel);
            return PartialView("GridViewPartial", BatchEditRepository.GridData);
        }
        /* save all changes to a data base in this action */
        public bool UpdateAllValues(MVCxGridViewBatchUpdateValues<GridDataItem, int> batchValues, string mainModel) {
            if (batchValues != null)
            {
                foreach (var item in batchValues.Insert)
                {
                    if (batchValues.IsValid(item))
                        BatchEditRepository.InsertNewItem(item, batchValues);
                    else
                        batchValues.SetErrorText(item, "Correct validation errors");
                }
                foreach (var item in batchValues.Update)
                {
                    if (batchValues.IsValid(item))
                        BatchEditRepository.UpdateItem(item, batchValues);
                    else
                        batchValues.SetErrorText(item, "Correct validation errors");
                }
                foreach (var itemKey in batchValues.DeleteKeys)
                {
                    BatchEditRepository.DeleteItem(itemKey, batchValues);
                }
            }
            bool result = false;
            if (!String.IsNullOrEmpty(mainModel))
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                MainModel m = jss.Deserialize<MainModel>(mainModel);
                //custom actions
                result = true;                
            }          
            return result && (batchValues == null || batchValues.EditorErrors.Count == 0); 
        }
        public ActionResult BatchUpdatePartial(MVCxGridViewBatchUpdateValues<GridDataItem, int> batchValues, string mainModel)
        {
           ViewData["SuccessFlag"] = UpdateAllValues(batchValues, mainModel);
            return PartialView("GridViewPartial", BatchEditRepository.GridData);
        }
        public ActionResult Success() {
            return View("Success");
        }
    
    }
}
