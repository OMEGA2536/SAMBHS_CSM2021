using SAMBHS.Common.BE.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMBHS.Windows.SigesoftIntegration.UI.Dtos
{
    public class ProtocolComponentList
    {
        public string ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string Porcentajes { get; set; }
        public string ProtocolComponentId { get; set; }

        public float? Price { get; set; }
        public string Operator { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int IsConditionalImc { get; set; }
        public decimal Imc { get; set; }
        public string IsConditional { get; set; }
        public int? IsAdditional { get; set; }
        public string ComponentTypeName { get; set; }
        public int ComponentTypeId { get; set; }

        public int GenderId { get; set; }
        public int GrupoEtarioId { get; set; }
        public int IsConditionalId { get; set; }
        public int OperatorId { get; set; }
        public int CategoryId { get; set; }

        public int UiIsVisibleId { get; set; }
        public int UiIndex { get; set; }
        public string IdUnidadProductiva { get; set; }

        public int i_IsDeleted { get; set; }
        public string v_CategoryName { get; set; }

        public int i_ConCargoA { get; set; }
    }


    public class ProtocoloComponentDto
    {
        public string v_ProtocolId { get; set; }
        public string v_ProtocolComponentId { get; set; }
        public string v_ComponentId { get; set; }
        public string v_ComponentName { get; set; }
        public string v_Gender { get; set; }
        public string v_IsConditional { get; set; }
        public string v_Operator { get; set; }
        public string v_ComponentTypeName { get; set; }
        public string v_CategoryName { get; set; }

        public string v_Porcentajes { get; set; }
        public int i_ServiceComponentStatusId { get; set; }
        public string v_ServiceComponentStatusName { get; set; }

        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }

        public float? r_Price { get; set; }
        public int? i_Age { get; set; }
        public int? i_GenderId { get; set; }
        public int? i_IsAdditional { get; set; }
        public int? i_GrupoEtarioId { get; set; }
        public int? i_IsConditionalId { get; set; }
        public int? i_OperatorId { get; set; }
        public int? i_ComponentTypeId { get; set; }

        public int? i_IsConditionalIMC { get; set; }
        public decimal? r_Imc { get; set; }


        public int? i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        //public int? i_isAdditional { get; set; }
        public int? i_CategoryId { get; set; }
    }

    public class MedicalExamList
    {
        public string v_MedicalExamId { get; set; }
        public string v_Name { get; set; }
        public string v_IsGroupName { get; set; }
        public int i_CategoryId { get; set; }
        public string v_CategoryName { get; set; }
        public int i_DiagnosableId { get; set; }
        public string v_DiagnosableName { get; set; }
        public int? i_ComponentTypeId { get; set; }
        public string v_ComponentTypeName { get; set; }
        public float? r_BasePrice { get; set; }
        public string v_IdUnidadProductiva { get; set; }

        public int i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }


        // jerarquia

        public List<Group> Groups { get; set; }
        public int i_UIIndex { get; set; }

        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }

        public string v_AuxiliaryExamId { get; set; }

        public string v_ServiceId { get; set; }



        public float? r_Price { get; set; }
        public string v_ComponentId { get; set; }
        public bool AtSchool { get; set; }
        public bool Adicional { get; set; }
        public bool Condicional { get; set; }
        public int Operador { get; set; }
        public int? i_Age { get; set; }
        public int? i_OperatorId { get; set; }
        public int? i_GenderId { get; set; }

    }
    
    public class Group
    {
        //public string v_Group { get; set; }
        public List<ComponentFieldsList> Controls { get; set; }
    }

    public class ComponentFieldsList
    {
        public string v_ComponentFieldId { get; set; }
        public string v_ComponentId { get; set; }
        public string v_Group { get; set; }
        public string v_TextLabel { get; set; }
        public int i_LabelWidth { get; set; }
        public string v_DefaultText { get; set; }
        public int i_ControlId { get; set; }
        public int i_GroupId { get; set; }
        public int i_ItemId { get; set; }
        public int i_ControlWidth { get; set; }
        public int i_HeightControl { get; set; }
        public int i_MaxLenght { get; set; }
        public int i_IsRequired { get; set; }
        public string v_IsRequired { get; set; }
        public int i_IsCalculate { get; set; }
        public int i_Order { get; set; }
        public int i_MeasurementUnitId { get; set; }
        public Single r_ValidateValue1 { get; set; }
        public Single r_ValidateValue2 { get; set; }
        public int i_Column { get; set; }

        public int? i_HasAutomaticDxId { get; set; }
        public string v_HasAutomaticDxComponentFieldsId { get; set; }

        public string v_MeasurementUnitName { get; set; }

        /// <summary>
        /// Indica si el campo es de fuente para algun calculo
        /// </summary>
        public int i_IsSourceFieldToCalculate { get; set; }
        /// <summary>
        /// Campo1 participante del calculo 
        /// </summary>
        public string v_SourceFieldToCalculateId1 { get; set; }
        /// <summary>
        /// Campo2 participante del calculo
        /// </summary>
        public string v_SourceFieldToCalculateId2 { get; set; }
        /// <summary>
        /// Campo donde se muestra el resultado del calculo
        /// </summary>
        public string v_TargetFieldOfCalculateId { get; set; }
        public string v_Formula { get; set; }
        public string v_FormulaChild { get; set; }

        public string v_SourceFieldToCalculateJoin { get; set; }

        public int i_IsDeleted { get; set; }
        public string v_CreationUser { get; set; }
        public string v_UpdateUser { get; set; }
        public DateTime? d_CreationDate { get; set; }
        public DateTime? d_UpdateDate { get; set; }

        public List<ComponentFieldValues> Values { get; set; }

        public List<TargetFieldOfCalculate> TargetFieldOfCalculateId { get; set; }
        public List<Formulate> Formula { get; set; }

        public int? i_RecordStatus { get; set; }
        public int? i_RecordType { get; set; }

        public string v_ComponentName { get; set; }

        public int? i_NroDecimales { get; set; }
        public int? i_ReadOnly { get; set; }
        public int? i_Enabled { get; set; }
    }

    public class Formulate
    {
        public string v_Formula { get; set; }
        public string v_TargetFieldOfCalculateId { get; set; }
    }

    public class TargetFieldOfCalculate
    {
        public string v_TargetFieldOfCalculateId { get; set; }
    }

    public class ComponentFieldValues
    {
        public string v_ComponentFieldValuesId { get; set; }
        public string v_ComponentFieldsId { get; set; }
        public string v_AnalyzingValue1 { get; set; }
        public string v_AnalyzingValue2 { get; set; }
        public int i_OperatorId { get; set; }
        public string v_Recommendation { get; set; }
        public int i_Cie10Id { get; set; }
        public string v_Restriction { get; set; }
        public string v_LegalStandard { get; set; }

        public int? i_IsAnormal { get; set; }
        public int? i_ValidationMonths { get; set; }
        public string v_ComponentId { get; set; }

        //public int i_IsDeleted { get; set; }
        //public string v_CreationUser { get; set; }
        //public string v_UpdateUser { get; set; }
        //public DateTime? d_CreationDate { get; set; }
        //public DateTime? d_UpdateDate { get; set; }

        public string v_DiseasesId { get; set; }
        public string v_DiseasesName { get; set; }  // diagnostico
        public string v_CIE10 { get; set; }
        public List<RecomendationList> Recomendations { get; set; }
        public List<RestrictionList> Restrictions { get; set; }
        public int? i_GenderId { get; set; }

    }
}
