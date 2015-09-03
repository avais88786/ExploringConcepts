using OpenGI.MVC.BusinessLines.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTOMReverseEngineerXML.Helpers
{
    public static class Extensions
    {
        public static object ProcessGroupVisibleBool(this object dataCapture)
        {
            
                ProcessLogicalGroups(ref dataCapture);
           
                
                return dataCapture;
        }

        private static void ProcessLogicalGroups(ref object propertyContents, System.Reflection.PropertyInfo property = null)
        {
            foreach (var propertyInThis in propertyContents.GetType().GetProperties())
            {
                if (propertyInThis.PropertyType.IsSubclassOf(typeof(LogicalGroup)))
                {
                    var propertyValue = ((LogicalGroup)propertyInThis.GetValue(propertyContents));
                    if (propertyValue == null)
                        continue;

                    propertyValue.GroupVisible = true;
                    var objects = (object)propertyValue;
                    ProcessLogicalGroups(ref objects, propertyInThis);
                }
                else if (propertyInThis.PropertyType.IsSubclassOf(typeof(RepeatGroupBase)))
                {
                    var propertyValue = ((RepeatGroupBase)propertyInThis.GetValue(propertyContents));
                    if (propertyValue == null)
                        continue;

                    propertyValue.GroupVisible = true;
                    var objects = (object)propertyValue;
                    ProcessLogicalGroups(ref objects, propertyInThis);
                }
                else if(propertyInThis.PropertyType.IsClass && !Filter(propertyInThis.PropertyType))
                {
                    if (propertyInThis.PropertyType.IsGenericType && propertyInThis.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var propertyValue = (System.Collections.IList)propertyInThis.GetValue(propertyContents);
                        int i = 0;
                        for (i = 0; i < propertyValue.Count; i++)
                        {
                            var objects = (object)propertyValue[i];
                            ProcessLogicalGroups(ref objects, null);
                            propertyValue[i] = objects;
                        }
                        //if (propertyValue.Count > 0)
                        //{
                        //    var objects = (object)propertyValue;
                        //    var genericType = propertyInThis.PropertyType.GenericTypeArguments[0];
                        //    ProcessLogicalGroups(ref objects, null);
                        //}
                        //else
                        //    continue;
                    }
                    else
                    {
                        var propertyValue = propertyInThis.GetValue(propertyContents);
                        ProcessLogicalGroups(ref propertyValue, propertyInThis);
                    }

                    
                }
                    

            }

            
        }

        static bool Filter(Type type)
        {
            return type.IsPrimitive || NoPrimitiveTypes.Contains(type.Name);
        }

        static readonly HashSet<string> NoPrimitiveTypes = new HashSet<string>() { "String", "DateTime", "Decimal" };

        
    }
}
