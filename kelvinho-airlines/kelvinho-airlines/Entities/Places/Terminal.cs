﻿using System;
using System.Collections.Generic;
using System.Text;

namespace kelvinho_airlines.Entities.Places
{
    public class Terminal : Place
    {
        public Terminal(List<CrewMember> crewMembers) : base()
        {
            Board(crewMembers);
        }

        public Terminal(List<CrewMember> crewMembers, SmartFortwo smartFortwo) : base()
        {
            if (!string.IsNullOrEmpty(smartFortwo.Location))
            {
                throw new ArgumentException("You can't set a smart fortwo that already had a location at constructor");
            }
            SetSmartFortwo(smartFortwo);
            Board(crewMembers);
        }

        public override void Board(List<CrewMember> crewMembers)
        {
            foreach(var crewMember in crewMembers)
            {
                CrewMembers.Add(crewMember);
            }
        }

        public override void Disembark(params CrewMember[] crewMembers)
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder crewMembers = new StringBuilder();
            foreach(var crewMember in CrewMembers)
            {
                crewMembers.AppendLine($"{crewMember.GetType().Name} : {crewMember.Name}");
            }

            return $"Terminal:\n\n{crewMembers}";
        }
    }
}