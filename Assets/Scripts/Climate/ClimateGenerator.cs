using System;
using System.Collections.Generic;
using UnityEngine;

public class ClimateGenerator
{
    public enum TemperatureTypes
    {
        POLAR,
        SUBPOLAR,
        BOREAL,
        COOL_TEMPERATE,
        WARM_TEMPERATE,
        SUBTROPICAL,
        TROPICAL
    }

    public enum MoistureTypes
    {
        SUPERARID,
        PERARID,
        ARID,
        SEMIARID,
        SUBHUMID,
        HUMID,
        PERHUMID,
        SUPERHUMID
    }
    
    public enum BiomeType
    {
        WATER,
        POLAR_ICE,
        POLAR_DESERT,
        SUBPOLAR_MOIST_TUNDRA,
        SUBPOLAR_WET_TUNDRA,
        SUBPOLAR_RAIN_TUNDRA,
        SUBPOLAR_DRY_TUNDRA,
        BOREAL_DESERT,
        BOREAL_DRY_SCRUB,
        BOREAL_MOIST_FOREST,
        BOREAL_WET_FOREST,
        BOREAL_RAIN_FOREST,
        COOL_TEMPERATE_MOIST_FOREST,
        COOL_TEMPERATE_WET_FOREST,
        COOL_TEMPERATE_RAIN_FOREST,
        COOL_TEMPERATE_STEPPES,
        COOL_TEMPERATE_DESERT,
        COOL_TEMPERATE_DESERT_SCRUB,
        WARM_TEMPERATE_MOIST_FOREST,
        WARM_TEMPERATE_WET_FOREST,
        WARM_TEMPERATE_RAIN_FOREST,
        WARM_TEMPERATE_THORN_SCRUB,
        WARM_TEMPERATE_DRY_FOREST,
        WARM_TEMPERATE_DESERT,
        WARM_TEMPERATE_DESERT_SCRUB,
        SUBTROPICAL_DESERT,
        SUBTROPICAL_DESERT_SCRUB,
        TROPICAL_DESERT,
        TROPICAL_DESERT_SCRUB,
        SUBTROPICAL_THORN_WOODLAND,
        TROPICAL_THORN_WOODLAND,
        TROPICAL_VERY_DRY_FOREST,
        SUBTROPICAL_DRY_FOREST,
        TROPICAL_DRY_FOREST,
        SUBTROPICAL_MOIST_FOREST,
        SUBTROPICAL_WET_FOREST,
        SUBTROPICAL_RAIN_FOREST,
        TROPICAL_MOIST_FOREST,
        TROPICAL_WET_FOREST,
        TROPICAL_RAIN_FOREST
    }

    private ClimateSettings settings;

    private NoiseFilter[] noiseFilters;

    private Dictionary<BiomeType, Color> biomesColors = new Dictionary<BiomeType, Color>();

    public MinMax moistureMinMax;

    public ClimateGenerator(ClimateSettings _settings, int _seed)
    {
        biomesColors[BiomeType.WATER] = Color.blue;
        biomesColors[BiomeType.POLAR_ICE] = new Color(255, 255, 255) / 255;
        biomesColors[BiomeType.POLAR_DESERT] = new Color(192, 192, 192) / 255;
        biomesColors[BiomeType.SUBPOLAR_MOIST_TUNDRA] = new Color(96, 128, 128) / 255;
        biomesColors[BiomeType.SUBPOLAR_WET_TUNDRA] = new Color(64, 128, 128) / 255;
        biomesColors[BiomeType.SUBPOLAR_RAIN_TUNDRA] = new Color(32, 128, 192) / 255;
        biomesColors[BiomeType.SUBPOLAR_DRY_TUNDRA] = new Color(128, 128, 128) / 255;
        biomesColors[BiomeType.BOREAL_DESERT] = new Color(160, 160, 128) / 255;
        biomesColors[BiomeType.BOREAL_DRY_SCRUB] = new Color(128, 160, 128) / 255;
        biomesColors[BiomeType.BOREAL_MOIST_FOREST] = new Color(96, 160, 128) / 255;
        biomesColors[BiomeType.BOREAL_WET_FOREST] = new Color(64, 160, 144) / 255;
        biomesColors[BiomeType.BOREAL_RAIN_FOREST] = new Color(32, 160, 192) / 255;
        biomesColors[BiomeType.COOL_TEMPERATE_MOIST_FOREST] = new Color(96, 192, 128) / 255;
        biomesColors[BiomeType.COOL_TEMPERATE_WET_FOREST] = new Color(64, 192, 144) / 255;
        biomesColors[BiomeType.COOL_TEMPERATE_RAIN_FOREST] = new Color(32, 192, 192) / 255;
        biomesColors[BiomeType.COOL_TEMPERATE_STEPPES] = new Color(128, 192, 128) / 255;
        biomesColors[BiomeType.COOL_TEMPERATE_DESERT] = new Color(192, 192, 128) / 255;
        biomesColors[BiomeType.COOL_TEMPERATE_DESERT_SCRUB] = new Color(160, 192, 128) / 255;
        biomesColors[BiomeType.WARM_TEMPERATE_MOIST_FOREST] = new Color(96, 224, 128) / 255;
        biomesColors[BiomeType.WARM_TEMPERATE_WET_FOREST] = new Color(64, 224, 144) / 255;
        biomesColors[BiomeType.WARM_TEMPERATE_RAIN_FOREST] = new Color(32, 224, 192) / 255;
        biomesColors[BiomeType.WARM_TEMPERATE_THORN_SCRUB] = new Color(160, 224, 128) / 255;
        biomesColors[BiomeType.WARM_TEMPERATE_DRY_FOREST] = new Color(128, 224, 128) / 255;
        biomesColors[BiomeType.WARM_TEMPERATE_DESERT] = new Color(224, 224, 128) / 255;
        biomesColors[BiomeType.WARM_TEMPERATE_DESERT_SCRUB] = new Color(192, 224, 128) / 255;
        biomesColors[BiomeType.SUBTROPICAL_DESERT] = new Color(240, 240, 128) / 255;
        biomesColors[BiomeType.SUBTROPICAL_DESERT_SCRUB] = new Color(224, 240, 128) / 255;
        biomesColors[BiomeType.TROPICAL_DESERT] = new Color(255, 255, 128) / 255;
        biomesColors[BiomeType.TROPICAL_DESERT_SCRUB] = new Color(224, 255, 128) / 255;
        biomesColors[BiomeType.SUBTROPICAL_THORN_WOODLAND] = new Color(176, 240, 128) / 255;
        biomesColors[BiomeType.TROPICAL_THORN_WOODLAND] = new Color(192, 255, 128) / 255;
        biomesColors[BiomeType.TROPICAL_VERY_DRY_FOREST] = new Color(160, 255, 128) / 255;
        biomesColors[BiomeType.SUBTROPICAL_DRY_FOREST] = new Color(128, 240, 128) / 255;
        biomesColors[BiomeType.TROPICAL_DRY_FOREST] = new Color(128, 255, 128) / 255;
        biomesColors[BiomeType.SUBTROPICAL_MOIST_FOREST] = new Color(96, 240, 128) / 255;
        biomesColors[BiomeType.SUBTROPICAL_WET_FOREST] = new Color(64, 240, 144) / 255;
        biomesColors[BiomeType.SUBTROPICAL_RAIN_FOREST] = new Color(32, 240, 176) / 255;
        biomesColors[BiomeType.TROPICAL_MOIST_FOREST] = new Color(96, 255, 128) / 255;
        biomesColors[BiomeType.TROPICAL_WET_FOREST] = new Color(64, 255, 144) / 255;
        biomesColors[BiomeType.TROPICAL_RAIN_FOREST] = new Color(32, 255, 160) / 255;

        settings = _settings;

        noiseFilters = new NoiseFilter[_settings.moistureNoiseLayers.Length];

        for (var i = 0; i < noiseFilters.Length; i++)
        {
            noiseFilters[i] = new NoiseFilter(_settings.moistureNoiseLayers[i].noiseSettings, _seed);
        }

        moistureMinMax = new MinMax();
    }

    public float Evaluate(Vector3 _pointOnUnitSphere)
    {
        float moisture = 0;

        for (var i = 0; i < noiseFilters.Length; i++)
        {
            if (settings.moistureNoiseLayers[i].enabled)
            {
                moisture += noiseFilters[i].Evaluate(_pointOnUnitSphere);
            }
        }

        moistureMinMax.AddValue(moisture);

        return moisture;
    }

    public Color GetColorFromBiome(float _elevation, float _moisture, float _latitude)
    {
        if (_latitude > 1 - settings.polesLatitude)
        {
            return biomesColors[BiomeType.POLAR_ICE];
        }
        
        if (_elevation < settings.oceanElevation)
        {
            return biomesColors[BiomeType.WATER];
        }
        
        if (_elevation > 0.95f)
        {
            return biomesColors[BiomeType.POLAR_ICE];
        }
        
        float temperature = Mathf.Lerp(settings.equatorTemperature, settings.polesTemperature, _latitude);

        temperature = temperature.Remap(-50.0f, settings.adjustBiomesWithOcean ? settings.oceanElevation : 0.0f, 50.0f, settings.adjustBiomesWithOcean ? 0.95f : 1.0f);

        float moisture = _moisture.Remap(0.0f, settings.adjustBiomesWithOcean ? settings.oceanElevation : 0.0f, 1.0f, 1.0f);

        TemperatureTypes temperatureType = GetTemperatureType(temperature * _elevation);
        MoistureTypes moistureType = GetMoistureType(moisture);
        BiomeType biomeType = GetBiomeType(temperatureType, moistureType);

        return biomesColors[biomeType];
    }

    BiomeType GetBiomeType(TemperatureTypes _temperatureType, MoistureTypes _moistureType)
    {
        switch (_temperatureType)
        {
            case TemperatureTypes.POLAR:
            {
                switch (_moistureType)
                {
                    case MoistureTypes.SUPERARID:
                    {
                        return BiomeType.POLAR_DESERT;
                    }

                    default:
                        return BiomeType.POLAR_ICE;
                }
            }

            case TemperatureTypes.SUBPOLAR:
            {
                switch (_moistureType)
                {
                    case MoistureTypes.SUPERARID:
                    {
                        return BiomeType.SUBPOLAR_DRY_TUNDRA;
                    }

                    case MoistureTypes.PERARID:
                    {
                        return BiomeType.SUBPOLAR_MOIST_TUNDRA;
                    }
                        
                    case MoistureTypes.ARID:
                    {
                        return BiomeType.SUBPOLAR_WET_TUNDRA;
                    }

                    default:
                    {
                        return BiomeType.SUBPOLAR_RAIN_TUNDRA;
                    }
                }
            }

            case TemperatureTypes.BOREAL:
            {
                switch (_moistureType)
                {
                    case MoistureTypes.SUPERARID:
                    {
                        return BiomeType.BOREAL_DESERT;
                    }

                    case MoistureTypes.PERARID:
                    {
                        return BiomeType.BOREAL_DRY_SCRUB;
                    }
                        
                    case MoistureTypes.ARID:
                    {
                        return BiomeType.BOREAL_MOIST_FOREST;
                    }

                    case MoistureTypes.SEMIARID:
                    {
                        return BiomeType.BOREAL_WET_FOREST;
                    }

                    default:
                    {
                        return BiomeType.BOREAL_RAIN_FOREST;
                    }
                }
            }

            case TemperatureTypes.COOL_TEMPERATE:
            {
                switch (_moistureType)
                {
                    case MoistureTypes.SUPERARID:
                    {
                        return BiomeType.COOL_TEMPERATE_DESERT;
                    }

                    case MoistureTypes.PERARID:
                    {
                        return BiomeType.COOL_TEMPERATE_DESERT_SCRUB;
                    }
                        
                    case MoistureTypes.ARID:
                    {
                        return BiomeType.COOL_TEMPERATE_STEPPES;
                    }

                    case MoistureTypes.SEMIARID:
                    {
                        return BiomeType.COOL_TEMPERATE_MOIST_FOREST;
                    }

                    case MoistureTypes.SUBHUMID:
                    {
                        return BiomeType.COOL_TEMPERATE_WET_FOREST;
                    }

                    default:
                    {
                        return BiomeType.COOL_TEMPERATE_RAIN_FOREST;
                    }
                }
            }

            case TemperatureTypes.WARM_TEMPERATE:
            {
                switch (_moistureType)
                {
                    case MoistureTypes.SUPERARID:
                    {
                        return BiomeType.WARM_TEMPERATE_DESERT;
                    }

                    case MoistureTypes.PERARID:
                    {
                        return BiomeType.WARM_TEMPERATE_DESERT_SCRUB;
                    }
                        
                    case MoistureTypes.ARID:
                    {
                        return BiomeType.WARM_TEMPERATE_THORN_SCRUB;
                    }

                    case MoistureTypes.SEMIARID:
                    {
                        return BiomeType.WARM_TEMPERATE_DRY_FOREST;
                    }

                    case MoistureTypes.SUBHUMID:
                    {
                        return BiomeType.WARM_TEMPERATE_MOIST_FOREST;
                    }

                    case MoistureTypes.HUMID:
                    {
                        return BiomeType.WARM_TEMPERATE_WET_FOREST;
                    }

                    default:
                    {
                        return BiomeType.WARM_TEMPERATE_RAIN_FOREST;
                    }
                }
            }

            case TemperatureTypes.SUBTROPICAL:
            {
                switch (_moistureType)
                {
                    case MoistureTypes.SUPERARID:
                    {
                        return BiomeType.SUBTROPICAL_DESERT;
                    }

                    case MoistureTypes.PERARID:
                    {
                        return BiomeType.SUBTROPICAL_DESERT_SCRUB;
                    }
                        
                    case MoistureTypes.ARID:
                    {
                        return BiomeType.SUBTROPICAL_THORN_WOODLAND;
                    }

                    case MoistureTypes.SEMIARID:
                    {
                        return BiomeType.SUBTROPICAL_DRY_FOREST;
                    }

                    case MoistureTypes.SUBHUMID:
                    {
                        return BiomeType.SUBTROPICAL_MOIST_FOREST;
                    }

                    case MoistureTypes.HUMID:
                    {
                        return BiomeType.SUBTROPICAL_WET_FOREST;
                    }

                    default:
                    {
                        return BiomeType.SUBTROPICAL_RAIN_FOREST;
                    }
                }
            }

            case TemperatureTypes.TROPICAL:
            {
                switch (_moistureType)
                {
                    case MoistureTypes.SUPERARID:
                    {
                        return BiomeType.TROPICAL_DESERT;
                    }

                    case MoistureTypes.PERARID:
                    {
                        return BiomeType.TROPICAL_DESERT_SCRUB;
                    }
                        
                    case MoistureTypes.ARID:
                    {
                        return BiomeType.TROPICAL_THORN_WOODLAND;
                    }

                    case MoistureTypes.SEMIARID:
                    {
                        return BiomeType.TROPICAL_VERY_DRY_FOREST;
                    }

                    case MoistureTypes.SUBHUMID:
                    {
                        return BiomeType.TROPICAL_DRY_FOREST;
                    }

                    case MoistureTypes.HUMID:
                    {
                        return BiomeType.TROPICAL_MOIST_FOREST;
                    }

                    case MoistureTypes.PERHUMID:
                    {
                        return BiomeType.TROPICAL_WET_FOREST;
                    }

                    default:
                    {
                        return BiomeType.TROPICAL_RAIN_FOREST;
                    }
                }
            }
            
            default:
                return BiomeType.TROPICAL_RAIN_FOREST;
        }
    }

    TemperatureTypes GetTemperatureType(float _temperature)
    {
        float targetTemperature = settings.temperatureDistribution.polar;
        
        if (_temperature < targetTemperature)
        {
            return TemperatureTypes.POLAR;
        }

        targetTemperature += settings.temperatureDistribution.subPolar;
        
        if (_temperature < targetTemperature)
        {
            return TemperatureTypes.SUBPOLAR;
        }

        targetTemperature += settings.temperatureDistribution.boreal;
        
        if (_temperature < targetTemperature)
        {
            return TemperatureTypes.BOREAL;
        }

        targetTemperature += settings.temperatureDistribution.coolTemperate;
        
        if (_temperature < targetTemperature)
        {
            return TemperatureTypes.COOL_TEMPERATE;
        }

        targetTemperature += settings.temperatureDistribution.warmTemperate;
        
        if (_temperature < targetTemperature)
        {
            return TemperatureTypes.WARM_TEMPERATE;
        }

        targetTemperature += settings.temperatureDistribution.subTropical;
        
        if (_temperature < targetTemperature)
        {
            return TemperatureTypes.SUBTROPICAL;
        }
        
        return TemperatureTypes.TROPICAL;
    }

    MoistureTypes GetMoistureType(float _moisture)
    {
        float targetMoisture = settings.moistureDistribution.superHumid;
        
        if (_moisture < targetMoisture)
        {
            return MoistureTypes.SUPERHUMID;
        }

        targetMoisture += settings.moistureDistribution.perHumid;
        
        if (_moisture < targetMoisture)
        {
            return MoistureTypes.PERHUMID;
        }

        targetMoisture += settings.moistureDistribution.humid;
        
        if (_moisture < targetMoisture)
        {
            return MoistureTypes.HUMID;
        }

        targetMoisture += settings.moistureDistribution.subHumid;
        
        if (_moisture < targetMoisture)
        {
            return MoistureTypes.SUBHUMID;
        }

        targetMoisture += settings.moistureDistribution.semiArid;
        
        if (_moisture < targetMoisture)
        {
            return MoistureTypes.SEMIARID;
        }

        targetMoisture += settings.moistureDistribution.arid;
        
        if (_moisture < targetMoisture)
        {
            return MoistureTypes.ARID;
        }

        targetMoisture += settings.moistureDistribution.perArid;
        
        if (_moisture < targetMoisture)
        {
            return MoistureTypes.PERARID;
        }
        
        return MoistureTypes.SUPERARID;
    }
}