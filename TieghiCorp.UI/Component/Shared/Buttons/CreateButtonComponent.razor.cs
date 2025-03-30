using Microsoft.AspNetCore.Components;
using MudBlazor;
using TieghiCorp.UI.Component.Department;
using TieghiCorp.UI.Component.Location;
using TieghiCorp.UI.Component.Personnel;
using TieghiCorp.UI.Component.Shared.Dialog;
using TieghiCorp.UI.Core.Models.Abstract;

namespace TieghiCorp.UI.Component.Shared.Buttons;

public partial class CreateButtonComponent<TModel> : ComponentBase where TModel : BaseModel
{
    #region Paramenters

    [Parameter]
    public string ButtonName { get; set; } = string.Empty;
    [Parameter]
    public bool IsDisable { get; set; } = false;
    [Parameter]
    public EventCallback OnSaveSuccess { get; set; }

    #endregion

    #region Services

    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    #endregion

    #region Methods

    private Task<IDialogReference> OpenDialogAsync()
    {
        var options = new DialogOptions { CloseOnEscapeKey = true };

        var parameters = new DialogParameters<DialogFormComponent<TModel>>
        {
            { x => x.OnSaveSuccess, OnSaveSuccess },
            { x => x.ChildContent, RenderFormComponent }
        };

        return DialogService.ShowAsync<DialogFormComponent<TModel>>($"Create {typeof(TModel).Name}", parameters, options);
    }

    private RenderFragment RenderFormComponent => builder =>
    {
        // Aqui determinamos qual componente de formulário renderizar baseado no TModel
        Type formComponentType = GetFormComponentType(typeof(TModel));

        builder.OpenComponent(0, formComponentType);
        builder.AddAttribute(1, "OnSaveSuccess", OnSaveSuccess);
        builder.CloseComponent();
    };

    private Type GetFormComponentType(Type modelType)
    {
        // Mapeia cada modelo para seu componente de formulário correspondente
        // Você pode fazer isso de forma mais sofisticada se tiver muitos modelos
        if (modelType == typeof(Core.Models.Location))
            return typeof(LocationFormComponent);

        if (modelType == typeof(Core.Models.Department))
            return typeof(DepartmentFormComponent);

        if (modelType == typeof(Core.Models.Personnel))
            return typeof(PersonnelFormComponent);

        // Adicione outros mapeamentos conforme necessário
        // if (modelType == typeof(Core.Models.Product))
        //    return typeof(ProductFormComponent);

        throw new NotImplementedException($"No form component implemented for {modelType.Name}");
    }

    #endregion
}