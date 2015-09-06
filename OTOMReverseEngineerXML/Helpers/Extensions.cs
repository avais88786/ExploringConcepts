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
        public static object ProcessGroupVisible(this object dataCapture)
        {
                SetGroupVisible(ref dataCapture);
                return dataCapture;
        }

        private static void SetGroupVisible(ref object propertyContents)
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
                    SetGroupVisible(ref objects);
                }
                else if (propertyInThis.PropertyType.IsSubclassOf(typeof(RepeatGroupBase)))
                {
                    var propertyValue = ((RepeatGroupBase)propertyInThis.GetValue(propertyContents));
                    if (propertyValue == null)
                        continue;

                    propertyValue.GroupVisible = true;
                    var objects = (object)propertyValue;
                    SetGroupVisible(ref objects);
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
                            ((RepeatGroupBase)objects).GroupVisible = true;
                            SetGroupVisible(ref objects);
                            propertyValue[i] = objects;
                        }
                    }
                    else
                    {
                        var propertyValue = propertyInThis.GetValue(propertyContents);
                        SetGroupVisible(ref propertyValue);
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
