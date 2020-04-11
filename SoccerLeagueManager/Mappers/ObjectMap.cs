using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SoccerLeagueManager.Mappers
{
    public static class ObjectMap
    {
        //extension method for copy object with new instance creation
        public static T MapObject<T>(this object obj)
        {
            T copiedObject = Activator.CreateInstance<T>();

            Type objectToMapDefinition = obj.GetType();
            Type copiedObjectType = copiedObject.GetType();
            foreach (PropertyInfo sourceProperty in objectToMapDefinition.GetProperties())
            {
                PropertyInfo targetProperty = copiedObjectType.GetProperty(sourceProperty.Name);
                if (targetProperty != null)
                    targetProperty.SetValue(copiedObject, sourceProperty.GetValue(obj));
            }
            return copiedObject;
        }

        //extension method for copy object with reference keeping
        public static void UpdateObject(this object objToUpdate, object objToMap)
        {
            Type objectToMapDefinition = objToMap.GetType();
            Type objectToUpdateDefinition = objToUpdate.GetType();
            foreach (PropertyInfo property in objectToMapDefinition.GetProperties())
            {
                PropertyInfo propertyToUpdate = objectToUpdateDefinition.GetProperty(property.Name);
                if (propertyToUpdate != null)
                    propertyToUpdate.SetValue(objToUpdate, property.GetValue(objToMap));
            }
        }
    }
}