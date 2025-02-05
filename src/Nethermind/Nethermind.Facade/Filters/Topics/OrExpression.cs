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

using System;
using System.Linq;
using Nethermind.Core;
using Nethermind.Core.Crypto;

namespace Nethermind.Blockchain.Filters.Topics
{
    public class OrExpression : TopicExpression, IEquatable<OrExpression>
    {
        private readonly TopicExpression[] _subexpressions;

        public OrExpression(params TopicExpression[] subexpressions)
        {
            _subexpressions = subexpressions;
        }

        public override bool Accepts(Keccak topic)
        {
            for (int i = 0; i < _subexpressions.Length; i++)
            {
                if (_subexpressions[i].Accepts(topic))
                {
                    return true;
                }
            }

            return false;
        }

        public override bool Accepts(ref KeccakStructRef topic)
        {
            for (int i = 0; i < _subexpressions.Length; i++)
            {
                if (_subexpressions[i].Accepts(ref topic))
                {
                    return true;
                }
            }

            return false;
        }

        public override bool Matches(Bloom bloom)
        {
            for (int i = 0; i < _subexpressions.Length; i++)
            {
                if (_subexpressions[i].Matches(bloom))
                {
                    return true;
                }
            }

            return false;
        }

        public override bool Matches(ref BloomStructRef bloom)
        {
            for (int i = 0; i < _subexpressions.Length; i++)
            {
                if (_subexpressions[i].Matches(ref bloom))
                {
                    return true;
                }
            }

            return false;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as OrExpression);
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new();
            for (int i = 0; i < _subexpressions.Length; i++)
            {
                hashCode.Add(_subexpressions[i].GetHashCode());
            }

            return hashCode.ToHashCode();
        }

        public bool Equals(OrExpression? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _subexpressions.SequenceEqual(other._subexpressions);
        }

        public override string ToString() => $"[{string.Join<TopicExpression>(',', _subexpressions)}]";
    }
}
