using MoreTags;
using System.Collections.Generic;

namespace MoreTags
{

/// <summary>
/// Utilidades para trabajar con Tags
/// </summary>
public static class TagUtilities
{   
    /// <summary>
    /// Convierte un patron a un arreglo de tags que hacen match con el
    /// </summary>
    /// <param name="pattern">Patron a obtener todos los tags relacionados</param>
    /// <returns>Lista de tags asociadas al patron</returns>
    public static string[] PatternToStrings(string pattern) {
        TagNames targetPatternInternal_;
        targetPatternInternal_ = pattern;
        List<string> resp = new List<string>();
        
        foreach (var tag in targetPatternInternal_)
        {
            resp.Add(tag);
        }

        return resp.ToArray();
    }
}

}