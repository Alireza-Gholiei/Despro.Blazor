//using AutoMapper;
//using AutoMapper.Internal;

//namespace Despro.Blazor.Base.BaseGenerals
//{
//    public class MapperTest
//    {
//        public static TOut Map<TIn, TOut>(TIn Item)
//        {
//            MapperConfiguration configuration = new(cfg =>
//            {
//                _ = cfg.CreateMap<TIn, TOut>();

//                cfg.Internal().ForAllMaps((typeMap, mappingExpr) =>
//                {
//                    IReadOnlyCollection<PropertyMap> ignoredPropMaps = typeMap.PropertyMaps;

//                    foreach (PropertyMap? map in ignoredPropMaps)
//                    {
//                        if (map.DestinationType.IsClass && map.DestinationType.Namespace != "System")
//                        {
//                            map.Ignored = true;
//                        }

//                        if (map.SourceType.IsClass && map.SourceType.Namespace != "System")
//                        {
//                            map.Ignored = true;
//                        }
//                    }
//                });
//            });

//            IMapper mapper = configuration.CreateMapper();

//            TOut dto = mapper.Map<TOut>(Item);

//            return dto;
//        }

//        public static List<TOut> MapList<TIn, TOut>(List<TIn> List)
//        {
//            List<TOut> newList = new();
//            if (List == null) return newList;
//            foreach (TIn VARIABLE in List)
//            {
//                TOut ConvertedValue = Map<TIn, TOut>(VARIABLE);
//                newList.Add(ConvertedValue);
//            }

//            return newList;
//        }

//        private static TOut Map2<TIn, TOut>(TIn source)
//        {
//            Dictionary<string, System.Reflection.PropertyInfo> inPropDict = typeof(TIn).GetProperties()
//                .Where(p => p.CanRead)
//                .ToDictionary(p => p.Name);
//            IEnumerable<System.Reflection.PropertyInfo> outProps = typeof(TOut).GetProperties()
//                .Where(p => p.CanWrite);
//            TOut destination = (TOut)Activator.CreateInstance(typeof(TOut));

//            foreach (System.Reflection.PropertyInfo outProp in outProps)
//            {
//                if (inPropDict.TryGetValue(outProp.Name, out System.Reflection.PropertyInfo inProp))
//                {
//                    object sourceValue = inProp.GetValue(source);

//                    try
//                    {
//                        outProp.SetValue(destination, sourceValue);
//                    }
//                    catch (Exception)
//                    {
//                    }
//                }
//            }

//            return destination;
//        }

//        private static List<TOut> MapList2<TIn, TOut>(List<TIn> List)
//        {
//            List<TOut> newList = new();
//            if (List == null) return newList;
//            foreach (TIn VARIABLE in List)
//            {
//                TOut ConvertedValue = Map<TIn, TOut>(VARIABLE);
//                newList.Add(ConvertedValue);
//            }

//            return newList;
//        }
//    }
//}
