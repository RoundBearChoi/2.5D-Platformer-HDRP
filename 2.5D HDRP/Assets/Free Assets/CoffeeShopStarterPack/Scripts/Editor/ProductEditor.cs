// ******------------------------------------------------------******
// ProductEditor.cs
//
// Author:
//       K.Sinan Acar <ksa@puzzledwizard.com>
//
// Copyright (c) 2019 PuzzledWizard
//
// ******------------------------------------------------------******
using UnityEngine;
using UnityEditor;
namespace PW
{
   [CustomPropertyDrawer(typeof(Product))]
    public class ProductEditor: PropertyDrawer
    {
        SerializedProperty propName;

        SerializedProperty orderID;

        SerializedProperty orderPrice;

        SerializedProperty productType;

        SerializedProperty addToPlateBeforeServed;

        SerializedProperty servedAsDifferentGameObject;

        SerializedProperty dontIncludeInThisScene;

        SerializedProperty plateOffset;

        SerializedProperty regenerateProduct;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            propName = property.FindPropertyRelative("productName");

            orderID = property.FindPropertyRelative("orderID");

            orderPrice = property.FindPropertyRelative("productPrice");

            productType = property.FindPropertyRelative("productType");

            addToPlateBeforeServed = property.FindPropertyRelative("addToPlateBeforeServed");

            plateOffset = property.FindPropertyRelative("plateOffset");

            servedAsDifferentGameObject = property.FindPropertyRelative("servedAsDifferentGameObject");

            dontIncludeInThisScene = property.FindPropertyRelative("dontIncludeInThisScene");

            regenerateProduct = property.FindPropertyRelative("RegenerateProduct");

            EditorGUI.BeginProperty(position, label, property);

            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();

            propName.stringValue = EditorGUILayout.TextField("Product Name: ", propName.stringValue);

            orderID.intValue = EditorGUILayout.IntField("OrderID", orderID.intValue);

            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Remove Product \n X "))
            {
                var pm = property.serializedObject.targetObject as ProductManager;
                bool result = pm.RemoveProduct(orderID.intValue);
                if (result)
                {
                    Undo.RegisterCompleteObjectUndo(pm as UnityEngine.Object, "Delete Product " + orderID);
                }
            }

            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel++;

            if (dontIncludeInThisScene != null)
                EditorGUILayout.PropertyField(dontIncludeInThisScene);
                //dontIncludeInThisScene.boolValue = EditorGUILayout.Toggle(dontIncludeInThisScene.boolValue);


            EditorGUILayout.PropertyField(productType);

            EditorGUILayout.PropertyField(property.FindPropertyRelative("productPrefab"));

            orderPrice.floatValue = EditorGUILayout.FloatField("productPrice", orderPrice.floatValue);

            //Some objects require to be added to plate before being served
            EditorGUILayout.PropertyField(addToPlateBeforeServed);

            //Some objects may require plateOffset on instantiate
            plateOffset.vector3Value = EditorGUILayout.Vector3Field("plateOffset", plateOffset!=null?plateOffset.vector3Value:Vector3.zero);


            if ((ProductType)productType.enumValueIndex == ProductType.Cookable)
            {

                //Some objects require raw and cooked or decorated final gameObject versions
                //We can set it with this property
                EditorGUILayout.PropertyField(servedAsDifferentGameObject);

            }
            else if((ProductType)productType.enumValueIndex == ProductType.ReadyToServe)
            {
                //No additional settings,
                //You may add specific properties here
                //for properties you added to readyToServe 
            }
            else if((ProductType)productType.enumValueIndex == ProductType.Heatable)
            {
                //Some objects require raw and cooked or decorated final gameObject versions
                //We can set it with this property
                EditorGUILayout.PropertyField(servedAsDifferentGameObject);
            }

            //Some objects may require regenerating after they're consumed
            
            regenerateProduct.boolValue = EditorGUILayout.Toggle("RegenerateProduct", regenerateProduct!=null? regenerateProduct.boolValue:false);

            EditorGUI.indentLevel--;


            EditorGUILayout.EndVertical();

            EditorGUI.EndProperty();
        }

    }
}
