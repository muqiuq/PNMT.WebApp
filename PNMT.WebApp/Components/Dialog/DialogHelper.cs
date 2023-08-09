using Microsoft.AspNetCore.Components;
using MudBlazor;


namespace PNMT.WebApp.Components.Dialog
{
  public static class DialogHelper
  {

    public static void ShowDeleteDialog(IDialogService dialogService, Func<Task> onAccept)
    {
      var parameters = new DialogParameters<OkCancelDialogComp>();
      parameters.Add(x => x.ContentText, "Do you really want to delete this entity? This process cannot be undone.");
      parameters.Add(x => x.ButtonText, "Delete");
      parameters.Add(x => x.Color, Color.Error);
      parameters.Add(x => x.OnAccept, onAccept);

      var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

      dialogService.Show<OkCancelDialogComp>("Delete", parameters, options);
    }
  }
}
