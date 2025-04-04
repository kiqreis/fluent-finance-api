using MudBlazor;

namespace FluentFinance.Web;

public static class Configuration
{
  public static MudTheme Theme = new()
  {
    Typography = new()
    {
      Default = new DefaultTypography()
      {
        FontFamily = ["Raleway", "sans-serif"]
      }
    },

    PaletteLight = new PaletteLight
    {
      Primary = "#FF9999",
      Secondary = "#FFD1CC",
      Tertiary = "#FFE6E6",
      Background = "#FFF8F5",
      Surface = "#FFFFFF",
      TextPrimary = "#4A3333"
    },
    PaletteDark = new PaletteDark
    {
      Primary = "#FF9999",
      Secondary = "#FFD1CC",
      Tertiary = "#FFE6E6",
      Background = "#2A1F1F",
      Surface = "#3C2F2F",
      TextPrimary = "#FFFFFF"
    }
  };
}