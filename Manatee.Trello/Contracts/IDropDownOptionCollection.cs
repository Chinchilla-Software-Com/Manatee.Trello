﻿using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of custom field drop down options.
	/// </summary>
	public interface IDropDownOptionCollection : IReadOnlyCollection<IDropDownOption>
	{
		/// <summary>
		/// Adds a new drop down option.
		/// </summary>
		/// <param name="text">The option's text.</param>
		/// <param name="position">The option's position.</param>
		/// <param name="color">(Optional) the option's color.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <returns>The <see cref="IDropDownOption"/> generated by Trello.</returns>
		Task<IDropDownOption> Add(string text, Position position, LabelColor? color = null,
		                          CancellationToken ct = default(CancellationToken));
	}
}