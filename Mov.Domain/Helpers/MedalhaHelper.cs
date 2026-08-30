using Mov.Domain.Enums;

namespace Mov.Domain.Helpers;

/// <summary>
/// Helper para cálculo de medalhas baseado em doações
/// </summary>
public static class MedalhaHelper
{
    /// <summary>
    /// Calcula a medalha apropriada para um total de doações
    /// </summary>
    public static string? GetMedalha(int total)
    {
        return total switch
        {
            >= 15000 => GetMedalhaName(MedalhaEnum.ReiHighlanderMestreEco),
            >= 10000 => GetMedalhaName(MedalhaEnum.MestreEco),
            >= 8000 => GetMedalhaName(MedalhaEnum.HighlanderVerde),
            >= 5000 => GetMedalhaName(MedalhaEnum.LendaDaReciclagem),
            >= 3500 => GetMedalhaName(MedalhaEnum.GrandeDaColeta),
            >= 2200 => GetMedalhaName(MedalhaEnum.IntermediarioVerdeMais),
            >= 1500 => GetMedalhaName(MedalhaEnum.IntermediarioVerde),
            >= 800 => GetMedalhaName(MedalhaEnum.DiscipuloDaColeta),
            >= 300 => GetMedalhaName(MedalhaEnum.AprendizEcologico),
            >= 100 => GetMedalhaName(MedalhaEnum.Iniciante),
            _ => null
        };
    }

    /// <summary>
    /// Retorna o nome legível da medalha
    /// </summary>
    private static string GetMedalhaName(MedalhaEnum medalha)
    {
        return medalha switch
        {
            MedalhaEnum.Iniciante => "Iniciante",
            MedalhaEnum.AprendizEcologico => "Aprendiz Ecológico",
            MedalhaEnum.DiscipuloDaColeta => "Discípulo da Coleta",
            MedalhaEnum.IntermediarioVerde => "Intermediário Verde",
            MedalhaEnum.IntermediarioVerdeMais => "Intermediário Verde Plus",
            MedalhaEnum.GrandeDaColeta => "Grande da Coleta",
            MedalhaEnum.LendaDaReciclagem => "Lenda da Reciclagem",
            MedalhaEnum.HighlanderVerde => "Highlander Verde",
            MedalhaEnum.MestreEco => "Mestre Eco",
            MedalhaEnum.ReiHighlanderMestreEco => "Rei Highlander Mestre Eco",
            _ => null
        };
    }
}
