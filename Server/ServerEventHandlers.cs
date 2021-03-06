﻿using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using CitizenFX.Core;
using IgiCore.Core.Extensions;
using IgiCore.Core.Models.Objects.Vehicles;
using IgiCore.Server.Extentions;
using IgiCore.Server.Models.Groups;
using IgiCore.Server.Models.Player;
using Newtonsoft.Json;
using Citizen = CitizenFX.Core.Player;

namespace IgiCore.Server
{
	public partial class Server
	{
		private static async void OnPlayerConnecting([FromSource] Citizen citizen, string playerName, CallbackDelegate kickReason)
		{
			var user = await User.GetOrCreate(citizen);

			user.Name = citizen.Name;

			Db.Users.AddOrUpdate(user);
			await Db.SaveChangesAsync();

			var session = await Session.Create(citizen, user);

			Log($"[CONNECT] [{session.Id}] Player \"{user.Name}\" connected from {session.IpAddress}");
		}

		private static async void OnPlayerDropped([FromSource] Citizen citizen, string disconnectMessage, CallbackDelegate kickReason)
		{
			var user = await User.GetOrCreate(citizen);

			var session = await Session.End(user, disconnectMessage);

			Log($"[DISCONNECT] [{session.Id}] Player \"{user.Name}\" disconnected: {disconnectMessage}");
		}

		private async void OnChatMessage(int playerId, string playerName, string message)
		{
			Citizen citizen = this.Players[playerId];

			var args = message.Split(' ').ToList();
			var command = args.First().ToLowerInvariant();
			args = args.Skip(1).ToList();

			switch (command)
			{
				case "/gps":
					Log("/gps command called");

					TriggerClientEvent(citizen, "igi:user:gps");

					break;
				case "/component":
					Log("/component command called");

					TriggerClientEvent(
						citizen,
						"igi:character:component:set",
						int.Parse(args[0]),
						int.Parse(args[1]),
						int.Parse(args[2]));

					break;
				case "/prop":
					Log("/prop command called");

					TriggerClientEvent(
						citizen,
						"igi:character:prop:set",
						int.Parse(args[0]),
						int.Parse(args[1]),
						int.Parse(args[2]));

					break;
				case "/car":
					Log("/car command called");

					Car car = new Car
					{
						Id = GuidGenerator.GenerateTimeBasedGuid(),
						Hash = (uint)VehicleHash.Elegy,
						Position = new Vector3 { X = -1038.121f, Y = -2738.279f, Z = 20.16929f },
						Seats = new List<VehicleSeat>
						{
							new VehicleSeat
								{Index = VehicleSeatIndex.LeftFront},
							new VehicleSeat
								{Index = VehicleSeatIndex.RightFront},
							new VehicleSeat
								{Index = VehicleSeatIndex.LeftRear},
							new VehicleSeat
								{Index = VehicleSeatIndex.RightRear}
						},
						Wheels = new List<VehicleWheel>
						{
							new VehicleWheel
							{
								Index = 0,
								IsBurst = false,
								Type = VehicleWheelType.Sport
							},
							new VehicleWheel
							{
								Index = 0,
								IsBurst = false,
								Type = VehicleWheelType.Sport
							},
							new VehicleWheel
							{
								Index = 0,
								IsBurst = false,
								Type = VehicleWheelType.Sport
							},
							new VehicleWheel
							{
								Index = 0,
								IsBurst = false,
								Type = VehicleWheelType.Sport
							}
						},
						Windows = new List<VehicleWindow>
						{
							new VehicleWindow
							{
								Index = VehicleWindowIndex.FrontLeftWindow,
								IsIntact = false,
								IsRolledDown = false
							},
							new VehicleWindow
							{
								Index = VehicleWindowIndex.FrontRightWindow,
								IsIntact = false,
								IsRolledDown = false
							},
							new VehicleWindow
							{
								Index = VehicleWindowIndex.BackLeftWindow,
								IsIntact = false,
								IsRolledDown = false
							},
							new VehicleWindow
							{
								Index = VehicleWindowIndex.BackRightWindow,
								IsIntact = false,
								IsRolledDown = false
							}
						},
						Doors = new List<VehicleDoor>
						{
							new VehicleDoor
								{Index = VehicleDoorIndex.FrontLeftDoor},
							new VehicleDoor
								{Index = VehicleDoorIndex.FrontRightDoor},
							new VehicleDoor
								{Index = VehicleDoorIndex.BackLeftDoor},
							new VehicleDoor
								{Index = VehicleDoorIndex.BackRightDoor},
							new VehicleDoor
								{Index = VehicleDoorIndex.Hood},
							new VehicleDoor
								{Index = VehicleDoorIndex.Trunk}
						}
					};

					Db.Cars.Add(car);
					await Db.SaveChangesAsync();

					Log($"Sending {car.Id}");

					TriggerClientEvent(citizen, "igi:car:spawn", JsonConvert.SerializeObject(car));

					break;
				case "/bike":
					Log("/bike command called");

					Bike bike = new Bike
					{
						Id = GuidGenerator.GenerateTimeBasedGuid(),
						Hash = (uint)VehicleHash.Double,
						Position = new Vector3 { X = -1038.121f, Y = -2738.279f, Z = 20.16929f }
					};

					Db.Bikes.Add(bike);
					await Db.SaveChangesAsync();

					Log($"Sending {bike.Id}");

					TriggerClientEvent(citizen, "igi:bike:spawn", JsonConvert.SerializeObject(bike));

					break;

				case "/group":
					Log("/group command called");

					if (args[0] == null) return;
					string groupName = args[0];
					await Group.Create(await citizen.ToLastCharacter(), groupName);

					break;
                case "/revive":
                    Log("/revive command called");
                    TriggerClientEvent(citizen, "igi:character:revive");
                    break;
				default:
					Log("Unknown command");

					break;
			}
		}
	}
}
