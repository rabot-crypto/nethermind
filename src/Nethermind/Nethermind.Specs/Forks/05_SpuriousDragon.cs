//  Copyright (c) 2021 Demerzel Solutions Limited
//  This file is part of the Nethermind library.
// 
//  The Nethermind library is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  The Nethermind library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with the Nethermind. If not, see <http://www.gnu.org/licenses/>.

using System.Threading;
using Nethermind.Core.Specs;

namespace Nethermind.Specs.Forks
{
    public class SpuriousDragon : TangerineWhistle
    {
        private static IReleaseSpec _instance;

        protected SpuriousDragon()
        {
            Name = "Spurious Dragon";
            MaxCodeSize = 24576;
            IsEip155Enabled = true;
            IsEip158Enabled = true;
            IsEip160Enabled = true;
            IsEip170Enabled = true;
        }

        public new static IReleaseSpec Instance => LazyInitializer.EnsureInitialized(ref _instance, () => new SpuriousDragon());
    }
}
