using PostModule.Application.Contract.StateApplication;
using PostModule.Domain.Services;
using PostModule.Domain.StateEntity;
using Shared.Application;

namespace PostModule.Application.Services
{
    internal class StateApplication : IStateApplication
    {
        private readonly IStateRepository _stateRepository;
        public StateApplication(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

		public bool ChangeStateClose(int id, List<int> stateCloses)
		{
            if (stateCloses.Count() < 1) return false;
            var state = _stateRepository.GetById(id);
            state.ChangeCloseStates(stateCloses);
            return _stateRepository.Save();
		}

		public OperationResult Create(CreateStateModel command)
        {
            if (_stateRepository.ExistBy(s => s.Title == command.Title))
                return new(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));
            State state = new(command.Title);
            if (_stateRepository.Create(state)) return new(true);
            return new(false,ValidationMessages.SystemErrorMessage, nameof(command.Title)); 
        }

        public OperationResult Edit(EditStateModel command)
        {
			if (_stateRepository.ExistBy(s => s.Title == command.Title && s.Id != command.Id))
				return new(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));
            State state = _stateRepository.GetById(command.Id);
            state.Edit(command.Title);
			if (_stateRepository.Save()) return new(true);
			return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
		}

        public bool ExistTitleForCreate(string title) =>
            _stateRepository.ExistBy(s => s.Title == title);

        public bool ExistTitleForEdit(string title, int id) =>
            _stateRepository.ExistBy(s => s.Title == title && s.Id != id);

        public List<StateViewModel> GetAll() =>
            _stateRepository.GetAllStateViewModel();

        public EditStateModel GetStateForEdit(int id)
        {
            return _stateRepository.GetStateForEdit(id);
        }
    }
}
