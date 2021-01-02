using MealOrdering.Client.Utils;
using MealOrdering.Shared.CustomExceptions;
using MealOrdering.Shared.DTO;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MealOrdering.Client.Pages.Users
{
    public class UserListProcess: ComponentBase
    {
        [Inject]
        public HttpClient Client { get; set; }

        [Inject]
        ModalManager ModalManager { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        protected List<UserDTO> UserList = new List<UserDTO>();


        protected async override Task OnInitializedAsync()
        {
            await LoadList();
        }


        protected void goCreateUserPage()
        {
            NavigationManager.NavigateTo("/users/add");
        }

        protected void goUpdateUserPage(Guid UserId)
        {
            NavigationManager.NavigateTo("/users/edit/" + UserId);
        }

        protected async Task DeleteUser(Guid Id)
        {
            bool confirmed = await ModalManager.ConfirmationAsync("Confirmation", "User will be deleted. Are you sure?");

            if (!confirmed) return;

            try
            {
                bool deleted = await Client.PostGetServiceResponseAsync<bool, Guid>("api/User/Delete", Id, true);

                await LoadList();
            }
            catch (ApiException ex)
            {
                await ModalManager.ShowMessageAsync("User Deletion Error", ex.Message);
            }
            catch (Exception ex)
            {
                await ModalManager.ShowMessageAsync("An Error", ex.Message);
            }
        }

        protected async Task LoadList()
        {
            try
            {
                UserList = await Client.GetServiceResponseAsync<List<UserDTO>>("api/User/Users", true);
            }
            catch (ApiException ex)
            {
                await ModalManager.ShowMessageAsync("Api Exception", ex.Message);
            }
            catch (Exception ex)
            {
                await ModalManager.ShowMessageAsync("Exception", ex.Message);
            }
        }

    }
}
