using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;
using TieghiCorp.UI.Core.Models.Abstract;

namespace TieghiCorp.UI.Component.Shared.Field;

public partial class TextFieldComponent<TModel, TValue> : ComponentBase where TModel : BaseModel
{
    private string InputValue { get; set; } = string.Empty;

    [Parameter]
    public TValue Value { get; set; }

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    [Parameter]
    public string Label { get; set; }
    [Parameter]
    public Expression<Func<TValue>> ForExpression { get; set; }
}