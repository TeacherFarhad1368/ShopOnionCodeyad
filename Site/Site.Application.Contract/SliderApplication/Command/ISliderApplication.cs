using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Contract.SliderApplication.Command
{
    public interface ISliderApplication
    {
        OperationResult Create(CreateSlider command);
        OperationResult Edit(EditSlider command);
        bool ActivationChange(int id);
        EditSlider GetForEdit(int id);
    }
}
