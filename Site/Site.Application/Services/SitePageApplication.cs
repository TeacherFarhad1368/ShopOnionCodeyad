using Azure;
using Shared.Application;
using Site.Application.Contract.SitePageApplication.Command;
using Site.Domain.SitePageAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Services;

internal class SitePageApplication : ISitePageApplication
{
	private readonly ISitePageRepository _sitePageRepository;

	public SitePageApplication(ISitePageRepository sitePageRepository)
	{
		_sitePageRepository = sitePageRepository;
	}

	public bool ActivationChange(int id)
	{
		var page = _sitePageRepository.GetById(id);
		page.ActivationChange();
		return _sitePageRepository.Save();
	}

	public OperationResult Create(CreateSitePage command)
	{
		if (_sitePageRepository.ExistBy(c => c.Title == command.Title.Trim()))
			return new(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));
		var slug = SlugUtility.GenerateSlug(command.Slug);
		if (_sitePageRepository.ExistBy(c => c.Slug == slug))
			return new(false, ValidationMessages.DuplicatedMessage, nameof(command.Slug));

		SitePage page = new(command.Title.Trim(), slug, command.Text);
		if (_sitePageRepository.Create(page)) return new(true);
		return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
	}

	public OperationResult Edit(EditSitePage command)
	{
		var page = _sitePageRepository.GetById(command.Id);
		if (_sitePageRepository.ExistBy(c => c.Title == command.Title.Trim() && c.Id != page.Id))
			return new(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));
		var slug = SlugUtility.GenerateSlug(command.Slug);
		if (_sitePageRepository.ExistBy(c => c.Slug == slug && c.Id != page.Id))
			return new(false, ValidationMessages.DuplicatedMessage, nameof(command.Slug));

		 page.Edit(command.Title.Trim(), slug, command.Text);
		if (_sitePageRepository.Create(page)) return new(true);
		return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
	}

	public EditSitePage GetForEdit(int id) =>
		_sitePageRepository.GetForEdit(id);
}
