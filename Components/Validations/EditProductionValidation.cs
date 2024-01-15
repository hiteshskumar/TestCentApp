using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ChargesWFM.UI.Models;
using ChargesWFM.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ChargesWFM.UI.Components.Validations
{
    public class EditProductionValidation : ComponentBase
    {
        private ValidationMessageStore messageStore;

        [CascadingParameter]
        public EditContext CurrentEditContext { get; set; }
       
        protected override Task OnInitializedAsync()
        {
            if (CurrentEditContext == null)
            {
                throw new Exception($"{nameof(EmployeeAccountDetails)} requires a cascading parameter of type {nameof(EditContext)}. You can use {nameof(EmployeeAccountDetails)} inside an {nameof(EditForm)}");
            }
            messageStore = new ValidationMessageStore(CurrentEditContext);
            CurrentEditContext.OnValidationRequested += (_, e) => messageStore.Clear();
            CurrentEditContext.OnFieldChanged += (_, e) => messageStore.Clear(e.FieldIdentifier);
            return Task.CompletedTask;
        }
        public async Task<bool> ValidateModifyTransaction(IEnumerable<EmployeeAccountDetails> accounts, List<BufferSectionDetails> BufferSections, IEnumerable<ProjectGroupFieldsData> placementFields)
        {           
            var errors = new Dictionary<string, List<string>>();
            foreach (var account in accounts)
            {
                if (string.IsNullOrEmpty(account.task?.Trim()))
                {
                    AddError(errors, nameof(EmployeeAccountDetails.task), "Task cannot be empty");
                }
                if (string.IsNullOrEmpty(account.subtask?.Trim()))
                {
                    AddError(errors, nameof(EmployeeAccountDetails.subtask), "Subtask cannot be empty");
                }
                ValidateReportingFields(errors, account, placementFields);
            }  
            
            foreach (var buffer in BufferSections)
            {
                ValidateBufferSectionFields(errors, buffer, placementFields);
            }  
            
            DisplayErrors(errors);
            return errors.Count == 0 ? true : false;
        }       
        private void ValidateReportingFields(Dictionary<string, List<string>> errors, EmployeeAccountDetails account, IEnumerable<ProjectGroupFieldsData> placementFields)
        {
            foreach (var field in placementFields)
            {
                var property = typeof(EmployeeAccountDetails).GetProperty(field.CodingField);
                if(property != null)
                {
                    var value = property.GetValue(account);
                    try
                    {
                        if (field.InputControl == "List" && field.IsMandatory == 1 && !ValidateMasterValue(property, value))
                        {
                            AddError(errors, field.CodingField, "Value for mandatory field must be valid");
                        }
                        else if (field.InputControl == "Text" && field.IsMandatory == 1 && !ValidateTextMasterValue(value))
                        {
                            AddError(errors, field.CodingField, "Value for mandatory field must be valid");
                        }
                        else if (field.InputControl == "Date" && !ValidateDateValue(value))
                        {
                            AddError(errors, field.CodingField, "Date field must be valid");
                        }
                        else if (field.IsMandatory == 1 && !ValidateFieldValue(property.PropertyType, value))
                        {
                            AddError(errors, field.CodingField, "Value for mandatory field must be valid");
                        }
                    }
                    catch (Exception ex)
                    {
                        AddError(errors, field.CodingField, "Please check the value");
                    }
                }
            }
        }
        private void ValidateBufferSectionFields(Dictionary<string, List<string>> errors, BufferSectionDetails account, IEnumerable<ProjectGroupFieldsData> placementFields)
        {
            foreach (var field in placementFields)
            {
                var property = typeof(BufferSectionDetails).GetProperty(field.CodingField);
                if(property != null)
                {
                    var value = property.GetValue(account);
                    try
                    {
                        if (field.InputControl == "List" && field.IsMandatory == 1 && !ValidateMasterValue(property, value))
                        {
                            AddError(errors, field.CodingField, "Value for mandatory field must be valid");
                        }
                        else if (field.InputControl == "Text" && field.IsMandatory == 1 && !ValidateTextMasterValue(value))
                        {
                            AddError(errors, field.CodingField, "Value for mandatory field must be valid");
                        }
                        else if (field.InputControl == "Date" && !ValidateDateValue(value))
                        {
                            AddError(errors, field.CodingField, "Date field must be valid");
                        }
                        else if (field.IsMandatory == 1 && !ValidateFieldValue(property.PropertyType, value))
                        {
                            AddError(errors, field.CodingField, "Value for mandatory field must be valid");
                        }
                    }
                    catch (Exception ex)
                    {
                        AddError(errors, field.CodingField, "Please check the value");
                    }
                }
            }
        }
        
        private bool ValidateFieldValue(Type type, object value)
        {
            if (Nullable.GetUnderlyingType(type) is Type underlyingType)
            {
                return ValidateFieldValue(underlyingType, value);
            }
            else if (value == null)
            {
                return false;
            }
            else if (type == typeof(string))
            {
                return !string.IsNullOrEmpty(value?.ToString().Trim());
            }

            return true;
        }

        private bool ValidateMasterValue(PropertyInfo property, object value)
        {
            if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
            {
                return value != null && Convert.ToInt32(value) > 0;
            }
            else if (property.PropertyType == typeof(string))
            {
                return ValidateTextMasterValue(value);
            }

            throw new Exception($"Invalid property type for a master field: {property.PropertyType}");
        }

        private bool ValidateTextMasterValue(object value)
        {
            return !string.IsNullOrEmpty(value?.ToString());
        }
        private bool ValidateDateValue(object value)
        {
            DateTime defaultdate = DateTime.Parse(Convert.ToDateTime("01/01/1900").ToShortDateString());
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                return true;
            }

            if (DateTime.TryParse(value.ToString(), out DateTime date) && date > defaultdate)
            {
                return true;
            }
            return false;
        }
        private void DisplayErrors(Dictionary<string, List<string>> errors)
        {
            foreach (var error in errors)
            {
                messageStore.Add(CurrentEditContext.Field(error.Key), error.Value);
            }

            CurrentEditContext.NotifyValidationStateChanged();
        }
        public void DisplayError(string fieldName, string error)
        {
            var fieldIdentifier = CurrentEditContext.Field(fieldName);
            if (GetErrors(fieldName).Any())
            {
                messageStore.Clear(fieldIdentifier);
            }

            messageStore.Add(fieldIdentifier, new List<string> { error });
            CurrentEditContext.NotifyValidationStateChanged();
        }
        private void AddError(Dictionary<string, List<string>> errors, string field, string errorMessage)
        {           
            if (errors.ContainsKey(field))
            {
                if (errors[field].Contains(errorMessage))
                {
                    return;
                }

                errors[field].Add(errorMessage);
            }
            else
            {
                errors.Add(field, new List<string> { errorMessage });
            }
        }
        public IEnumerable<string> GetErrors(string fieldName)
        {            
            if (string.IsNullOrEmpty(fieldName))
            {
                return Enumerable.Empty<string>();
            }
            return messageStore[CurrentEditContext.Field(fieldName)];
        }
        public void ClearErrors()
        {
            messageStore.Clear();
            CurrentEditContext.NotifyValidationStateChanged();
        }
        public void ClearError(string fieldName)
        {
            messageStore.Clear(CurrentEditContext.Field(fieldName));
            CurrentEditContext.NotifyValidationStateChanged();
        }
    }
}
