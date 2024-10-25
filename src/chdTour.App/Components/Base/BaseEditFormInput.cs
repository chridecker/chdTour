using chdTour.App.Components.Inputs;
using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;

namespace chdTour.App.Components.Base
{
    public class BaseEditFormInput<TRepo, T, TValue> : ComponentBase 
        where TRepo : IBaseRepository<T>
        where T : class
    {
        [CascadingParameter] private EditFormLayout<TRepo, T> _layout { get; set; } = default!;

        [Parameter] public T Value { get; set; }
        [Parameter] public Expression<Func<T, TValue>> Property { get; set; }
        [Parameter] public string? Title { get; set; } = null;
        [Parameter] public bool Disabled { get; set; } = false;
        [Parameter] public EventCallback<TValue> OnChange { get; set; }

        private PropertyInfo _propertyInfo;

        protected override void OnParametersSet()
        {
            var memberExpression = this.Property.Body as MemberExpression ?? throw new ArgumentException("The expression is not a member access expression.", nameof(this.Property));
            this._propertyInfo = memberExpression.Member as PropertyInfo ?? throw new ArgumentException("The member access expression does not access a property.", nameof(this.Property));

            base.OnParametersSet();
        }

        protected TValue? GetValue()
        {
            if (this.Value is null) { return default; }

            return (TValue?)this._propertyInfo.GetValue(this.Value);
        }

        protected async Task ValueChanged(TValue? value)
        {
            var current = this.GetValue();
            if (current is null && value is null) { return; }
            if (current is not null && current.Equals(value)) { return; }

            this._propertyInfo?.SetValue(this.Value, value);

            await this._layout.InvokeStateChange();
            await this.OnChange.InvokeAsync(value);
        }
    }
}
