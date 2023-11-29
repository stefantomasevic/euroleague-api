using AutoMapper;
using AutoMapper.QueryableExtensions;
using Euroleague.DTO;
using Euroleague.Models;


namespace Euroleague.Mappings
{
    public class AutoMapperprofile : Profile
    {
        public AutoMapperprofile()
        {
            CreateMap<Player, PlayerDTO>();

            CreateMap<Team, TeamDTO>()
                .ForMember(dest => dest.Players, opt => opt.MapFrom(src => src.Players));

            CreateMap<TeamManipulationDTO, Team>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.Coach, opt => opt.MapFrom(src => src.Coach))
            .ForMember(dest => dest.Arena, opt => opt.MapFrom(src => src.Arena))
            .ForMember(dest => dest.Players, opt => opt.Ignore());

            CreateMap<Team, TeamManipulationDTO>();

            CreateMap<Game, GameDTO>()
                .ForMember(dest => dest.HomeTeam, opt => opt.MapFrom(src => src.HomeTeam.Name))
                .ForMember(dest => dest.GuestTeam, opt => opt.MapFrom(src => src.GuestTeam.Name))
                .ForMember(dest => dest.HomeLogo, opt => opt.MapFrom(src => src.HomeTeam.LogoPath))
                .ForMember(dest => dest.GuestLogo, opt => opt.MapFrom(src => src.GuestTeam.LogoPath));


            CreateMap<Statistic, PlayerStatsDTO>()
    .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.PlayerId))
    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Player.FirstName))
    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Player.LastName)) // Ovo pretpostavlja da postoji navigaciono svojstvo iz Statistic u Player
    .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points))
     .ForMember(dest => dest.Rebounds, opt => opt.MapFrom(src => src.Rebounds))
      .ForMember(dest => dest.Fouls, opt => opt.MapFrom(src => src.Fouls));


            CreateMap<Game, GameDetailsDTO>()
     .ForMember(dest => dest.HomeTeam, opt => opt.MapFrom(src => src.HomeTeam.Name))
     .ForMember(dest => dest.GuestTeam, opt => opt.MapFrom(src => src.GuestTeam.Name))
     .ForMember(dest => dest.HomeLogo, opt => opt.MapFrom(src => src.HomeTeam.LogoPath))
     .ForMember(dest => dest.GuestLogo, opt => opt.MapFrom(src => src.GuestTeam.LogoPath))
     .ForMember(dest => dest.HomePlayerStats, opt => opt.MapFrom(src => src.HomeTeam.Players.SelectMany(p => p.Statistics)))
     .ForMember(dest => dest.GuestPlayerStats, opt => opt.MapFrom(src => src.GuestTeam.Players.SelectMany(p => p.Statistics)));

            CreateMap<Player, LineupPlayerDTO>();

            CreateMap<Game, LineupDTO>()
    .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.Id))
    .ForMember(dest => dest.HomeTeam, opt => opt.MapFrom(src => src.HomeTeam.Name))
    .ForMember(dest => dest.GuestTeam, opt => opt.MapFrom(src => src.GuestTeam.Name))
    .ForMember(dest => dest.HomeId, opt => opt.MapFrom(src => src.HomeTeam.Id))
    .ForMember(dest => dest.GuestId, opt => opt.MapFrom(src => src.GuestTeam.Id))
    .ForMember(dest => dest.GuestLogo, opt => opt.MapFrom(src => src.GuestTeam.LogoPath))
    .ForMember(dest => dest.HomeLogo, opt => opt.MapFrom(src => src.HomeTeam.LogoPath))
    .ForMember(dest => dest.HomePlayers, opt => opt.MapFrom(src => src.HomeTeam.Players.Select(player => new LineupPlayerDTO
    {
        Id = player.Id,
        FirstName = player.FirstName,
        LastName = player.LastName,
    })))
    .ForMember(dest => dest.GuestPlayers, opt => opt.MapFrom(src => src.GuestTeam.Players.Select(player => new LineupPlayerDTO
    {
        Id = player.Id,
        FirstName = player.FirstName,
        LastName = player.LastName,
    })));




            CreateMap<ManipulationLineUpDTO, LineupDTO>()
           .ForMember(dest => dest.HomePlayers, opt => opt.MapFrom(src => src.HomePlayers))
           .ForMember(dest => dest.GuestPlayers, opt => opt.MapFrom(src => src.GuestPlayers))
           .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.GameId));

            CreateMap<PlayerManipulationDTO, Player>()
          .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignoriši Id, pošto će Entity Framework postaviti vrednost
          .ForMember(dest => dest.Team, opt => opt.Ignore()) // Ignoriši Team, pošto ćeš postaviti TeamId posebno
          .ForMember(dest => dest.Statistics, opt => opt.Ignore());


            CreateMap<Player, PlayerManipulationDTO>();

        }


    }



}
