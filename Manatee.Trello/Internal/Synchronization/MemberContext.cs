/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		MemberContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		MemberContext
	Purpose:		Provides a data context for a member.

***************************************************************************************/
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class MemberContext : SynchronizationContext<IJsonMember>
	{
		static MemberContext()
		{
			_properties = new Dictionary<string, Property<IJsonMember>>
				{
					{"AvatarSource", new Property<IJsonMember, AvatarSource>(d => d.AvatarSource, (d, o) => d.AvatarSource = o)},
					{"Bio", new Property<IJsonMember, string>(d => d.Bio, (d, o) => d.Bio = o)},
					{"Email", new Property<IJsonMember, string>(d => d.Email, (d, o) => d.Email = o)},
					{"FullName", new Property<IJsonMember, string>(d => d.FullName, (d, o) => d.FullName = o)},
					{"Id", new Property<IJsonMember, string>(d => d.Id, (d, o) => d.Id = o)},
					{"Initials", new Property<IJsonMember, string>(d => d.Initials, (d, o) => d.Initials = o)},
					{"IsConfirmed", new Property<IJsonMember, bool?>(d => d.Confirmed, (d, o) => d.Confirmed = o)},
					{"Similarity", new Property<IJsonMember, int?>(d => d.Similarity, (d, o) => d.Similarity = o)},
					{"Status", new Property<IJsonMember, MemberStatus>(d => d.Status, (d, o) => d.Status = o)},
					{"Trophies", new Property<IJsonMember, List<string>>(d => d.Trophies, (d, o) => d.Trophies = o == null ? null : o.ToList())},
					{"Url", new Property<IJsonMember, string>(d => d.Url, (d, o) => d.Url = o)},
					{"UserName", new Property<IJsonMember, string>(d => d.Username, (d, o) => d.Username = o)},
				};
		}
		public MemberContext(string id)
		{
			Data.Id = id;
		}

		protected override IJsonMember GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonMember>(TrelloAuthorization.Default, endpoint);
			return newData;
		}
		protected override void SubmitData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, Data);
		}
	}
}