using Car.Inventory;

namespace Car.Abilities
{
    public class AbilitiesController : ControllerBase, IAbilitiesController
    {
        private IRepository<int, IAbility> abilitiesRepository;
        private IAbilityActivator abilityActivator;
        private IInventoryController inventoryController;
        private IAbilitiesView abilitiesView;
        private IReadOnlyProperty abilitiesOpener;

        public AbilitiesController(IRepository<int, IAbility> _abilitiesRepository, IAbilityActivator _abilityActivator, IInventoryController _inventoryController, IAbilitiesView _abilitiesView, IReadOnlyProperty _abilitiesOpener)
        {
            abilitiesRepository = _abilitiesRepository;
            abilityActivator = _abilityActivator;
            inventoryController = _inventoryController;
            abilitiesView = _abilitiesView;
            abilitiesOpener = _abilitiesOpener;
            abilitiesView.Selected += AbilitySelected;
            //abilitiesView.Display(inventoryModel.EquippedItems, abilitiesRepository);
            abilitiesOpener.Subscribe(ShowOrHide);
        }

        protected override void OnDispose()
        {
            abilitiesView.Selected -= AbilitySelected;
            abilitiesView.Dispose();
        }

        private void ShowOrHide()
        {
            if(abilitiesView.IsDisplayed)
                abilitiesView.Hide();
            else
                abilitiesView.Display(inventoryController.EquippedItems, abilitiesRepository);
        }

        private void AbilitySelected(IAbility ability)
        {
            ability.Apply(abilityActivator);
        }
    }
}
