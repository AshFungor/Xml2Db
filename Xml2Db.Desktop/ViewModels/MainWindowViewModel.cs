using Microsoft.Extensions.Logging;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Xml2Db.Models;

namespace Xml2Db.Desktop.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly ILogger _logger;
        private readonly Func<Context> _contextFactory;

        public MainWindowViewModel(ILogger<MainWindowViewModel> logger, Func<Context> contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;
            WaresViewModel = new WaresViewModel(contextFactory);
            ColorsViewModel = new ColorsViewModel(contextFactory);
            OriginsViewModel = new OriginsViewModel(contextFactory);
            ArrivalViewModel = new ArrivalViewModel(contextFactory);
            SellsViewModel = new SellsViewModel(contextFactory);
        }

        public WaresViewModel WaresViewModel { get; }
        public ColorsViewModel ColorsViewModel { get; }
        public OriginsViewModel OriginsViewModel { get; }
        public ArrivalViewModel ArrivalViewModel { get; }
        public SellsViewModel SellsViewModel { get; }

        public void Load(string path)
        {
            _logger.LogInformation("Начат обмен данными XML из файла {path}", path);
            XmlSerializer serializer = new XmlSerializer(typeof(Message));
            using (var reader = new FileStream(path, FileMode.Open))
            {
                using (var context = _contextFactory())
                {
                    try
                    {
                        var msg = serializer.Deserialize(reader) as Message;
                        foreach (var obj in msg.Body)
                        {
                            switch (obj)
                            {
                                case ObjectDeletion deletion:
                                    _logger.LogInformation("Удаление объекта {ref}...", deletion.Ref.Value);
                                    switch (deletion.Ref)
                                    {
                                        case OriginReference origRef:
                                            {
                                                var dbRef = context.Origins.Find(origRef.Value);
                                                if (dbRef != null)
                                                {
                                                    context.Origins.Remove(dbRef);
                                                    _logger.LogInformation("Производитель {ref} ({description}) удалён", dbRef.Ref, dbRef.Description);
                                                }
                                                else
                                                {
                                                    _logger.LogWarning("Производитель {ref} отсутствует в базе!", deletion.Ref.Value);
                                                }
                                            }
                                            break;
                                        case ColorReference colRef:
                                            {
                                                var dbRef = context.Colors.Find(colRef.Value);
                                                if (dbRef != null)
                                                {
                                                    context.Colors.Remove(dbRef);
                                                    _logger.LogInformation("Цвет {ref} ({description}) удалён", dbRef.Ref, dbRef.Description);
                                                }
                                                else
                                                {
                                                    _logger.LogWarning("Цает {ref} отсутствует в базе!", deletion.Ref.Value);
                                                }
                                            }
                                            break;
                                        case WareReference wareRef:
                                            {
                                                var dbRef = context.Wares.Find(wareRef.Value);
                                                if (dbRef != null)
                                                {
                                                    context.Wares.Remove(dbRef);
                                                    _logger.LogInformation("Товар {ref} ({description}) удалён", dbRef.Ref, dbRef.Description);
                                                }
                                                else
                                                {
                                                    _logger.LogWarning("Товар {ref} отсутствует в базе!", deletion.Ref.Value);
                                                }
                                            }
                                            break;
                                        case ArrivalReference arrRef:
                                            {
                                                var dbRef = context.Arrivals.Find(arrRef.Value);
                                                if (dbRef != null)
                                                {
                                                    context.Arrivals.Remove(dbRef);
                                                    _logger.LogInformation("Поступление {ref} удалено", arrRef.Value);
                                                }
                                                else
                                                {
                                                    _logger.LogWarning("Поступление {ref} отсутствует в базе!", arrRef.Value);
                                                }
                                            }
                                            break;
                                        case SellReference sellRef:
                                            {
                                                var dbRef = context.Arrivals.Find(sellRef.Value);
                                                if (dbRef != null)
                                                {
                                                    context.Sells.Remove(dbRef);
                                                    _logger.LogInformation("Продажа {ref} удалена", sellRef.Value);
                                                }
                                                else
                                                {
                                                    _logger.LogWarning("Продажа {ref} отсутствует в базе!", sellRef.Value);
                                                }
                                            }
                                            break;
                                    }
                                    break;
                                case Color color:
                                    _logger.LogInformation("Добавление цвета {ref} ({description})...", color.Ref, color.Description);
                                    {
                                        var dbRef = context.Colors.Find(color.Ref);
                                        if (dbRef == null)
                                        {
                                            context.Colors.Add(color);
                                            _logger.LogInformation("Цвета {ref} ({description}) добавлен", color.Ref, color.Description);
                                        }
                                        else
                                        {
                                            _logger.LogWarning("Цвет {ref} ({description}) уже есть в базе!", color.Ref, color.Description);
                                        }
                                    }
                                    break;
                                case Origin origin:
                                    _logger.LogInformation("Добавление производителя {ref} ({description})...", origin.Ref, origin.Description);
                                    {
                                        var dbRef = context.Origins.Find(origin.Ref);
                                        if (dbRef == null)
                                        {
                                            context.Origins.Add(origin);
                                            _logger.LogInformation("Производитель {ref} ({description}) добавлен", origin.Ref, origin.Description);
                                        }
                                        else
                                        {
                                            _logger.LogWarning("Производитель {ref} ({description}) уже есть в базе!", origin.Ref, origin.Description);
                                        }
                                    }
                                    break;
                                case Ware ware:
                                    _logger.LogInformation("Добавление товара {ref} ({description})...", ware.Ref, ware.Description);
                                    {
                                        var dbRef = context.Wares.Find(ware.Ref);
                                        if (dbRef == null)
                                        {
                                            context.Wares.Add(ware);
                                            _logger.LogInformation("Товар {ref} ({description}) добавлен", ware.Ref, ware.Description);
                                        }
                                        else
                                        {
                                            _logger.LogWarning("Товар {ref} ({description}) уже есть в базе!", ware.Ref, ware.Description);
                                        }
                                    }
                                    break;
                                case RecordSet recordSet:
                                    switch (recordSet.Filter.Recorder)
                                    {
                                        case ArrivalReference arrival:
                                            _logger.LogInformation("Добавление информации о поступлении {ref}...", arrival.Value);
                                            {
                                                var dbRef = context.Arrivals.Find(arrival.Value);
                                                if (dbRef == null)
                                                {
                                                    context.Arrivals.Add(arrival);
                                                    _logger.LogInformation("Информация о поступлении {ref} добавлена", arrival.Value);
                                                }
                                                else
                                                {
                                                    _logger.LogWarning("Информация о поступлении {ref} уже есть в базе!", arrival.Value);
                                                }
                                            }
                                            break;
                                        case SellReference sell:
                                            _logger.LogInformation("Добавление информации о продаже {ref}...", sell.Value);
                                            {
                                                var dbRef = context.Sells.Find(sell.Value);
                                                if (dbRef == null)
                                                {
                                                    context.Sells.Add(sell);
                                                    _logger.LogInformation("Информация о продаже {ref} добавлена", sell.Value);
                                                }
                                                else
                                                {
                                                    _logger.LogWarning("Информация о продаже {ref} уже есть в базе!", sell.Value);
                                                }
                                            }
                                            break;
                                    }
                                    break;
                            }
                        }
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError("Ошибка разбора XML-файла: {error}", e.Message);
                        throw;
                    }
                }
            }
            _logger.LogInformation("Обмен данными завершён", path);
            WaresViewModel.Load();
            OriginsViewModel.Load();
            ColorsViewModel.Load();
            ArrivalViewModel.Load();
            SellsViewModel.Load();
        }
    }
}
