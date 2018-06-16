﻿using System;
using IgiCore.SDK.Core.Helpers;

namespace Roleplay.Vehicles.Core.Models
{
    public class VehicleDoor
    {
        public Guid Id { get; set; }
        public VehicleDoorIndex Index { get; set; }
        public bool IsOpen { get; set; } = false;
        public bool IsClosed => !this.IsOpen;
        public bool IsBroken { get; set; } = false;
		public float Angle { get; set; }

        public VehicleDoor() { this.Id = GuidGenerator.GenerateTimeBasedGuid(); }
    }
}