using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RecipeApp.Model;

namespace RecipeApp.Components;

public partial class MenuView
{
  private Menu? menu;
  [Parameter]
  public int Id { get; set; }

  protected override Task OnInitializedAsync()
  {
    this.menu = this.MenuService.GetMenu(this.Id);

    return Task.CompletedTask;
  }

  private void EditMenu()
  {
    this.NavigationManager.NavigateTo($"/menu/edit/{this.Id}");
  }

  private async Task ConfirmDelete()
  {
    var confirmed = await this.Js.InvokeAsync<bool>("confirm", "Удалить меню?");
    if (confirmed)
    {
      this.MenuService.DeleteMenuAsync(this.Id);
      this.NavigationManager.NavigateTo("/");
    }
  }

  private void GoBack()
  {
    this.NavigationManager.NavigateTo("/");
  }

  private void NavigateToRecipe(int recipeId)
  {
    if (recipeId > 0)
    {
      NavigationManager.NavigateTo($"/recipes/{recipeId}");
    }
  }
}