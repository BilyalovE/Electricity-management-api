using Microsoft.Extensions.DependencyInjection;
using MyWebService.Core.Services;

public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var organizationService = services.GetRequiredService<OrganizationService>();
                var childOrganizationService = services.GetRequiredService<ChildOrganizationService>();
                var supplyPointService = services.GetRequiredService<SupplyPointService>();
                var consumptionObjectService = services.GetRequiredService<ConsumptionObjectService>();
                var measurementPointService = services.GetRequiredService<MeasurementPointService>();
                var electricityMeterService = services.GetRequiredService<ElectricityMeterService>();
                var currentTransformerService = services.GetRequiredService<CurrentTransformerService>();
                var voltageTransformerService = services.GetRequiredService<VoltageTransformerService>();
                var calculationMeterService = services.GetRequiredService<CalculationMeterService>();

                // Создаем тестовые данные
                
                // ---------------------------------------------------------------------------------------------------------
                // Организации
                var organization1 = new Organization { Name = "Энерго Москва", Address = "Бутлерова" };
                var organization2 = new Organization { Name = "Энерго Новороссийск", Address = "Полярная" };
                // var organization3 = new Organization { Name = "Энерго Краснодар", Address = "Ленина 51" };
                organizationService.Add(organization1);
                organizationService.Add(organization2);
                
                // ---------------------------------------------------------------------------------------------------------
                // Дочерние организации
                // Дочерняя организация для главной организации 1
                var childOrganization1_1 = new ChildOrganization
                {
                    Name = "Энерго Москва-Юг",
                    OrganizationId = organization1.Id, Address = "Бутлерова 1"
                };
                var childOrganization1_2 = new ChildOrganization
                {
                    Name = "Энерго Москва-Север",
                    OrganizationId = organization1.Id, 
                    Address = "Бутлерова 2"
                };
                childOrganizationService.Add(childOrganization1_1);
                childOrganizationService.Add(childOrganization1_2);
             
                // Дочерняя организация для главной организации 2
                var childOrganization2_1 = new ChildOrganization
                {
                    Name = "Энерго Новороссийск-Юг",
                    OrganizationId = organization2.Id, 
                    Address = "Полярная 1"
                };
                var childOrganization2_2 = new ChildOrganization
                {
                    Name = "Энерго Новороссийск-Север",
                    OrganizationId = organization2.Id, 
                    Address = "Полярная 2"
                };
                childOrganizationService.Add(childOrganization2_1);
                childOrganizationService.Add(childOrganization2_2);
                
                // ---------------------------------------------------------------------------------------------------------
                // Для каждой дочерней организации создаем объекты потребления
                
                // У первой дочерней организации 2 объекта потребления
                // объекты потребления главной организации 1 - дочерней организации 1
                var consumptionObject1_1_1 = new ConsumptionObject
                {
                    Name = "ТЦ Энерго Москва-Юг 1",
                    Address = "Бутлерова 1_1" ,
                    ChildOrganizationId = childOrganization1_1.Id,
                };
                var consumptionObject1_1_2 = new ConsumptionObject
                {
                    Name = "ТЦ Энерго Москва-Юг 2",
                    Address = "Бутлерова 1_2" ,
                    ChildOrganizationId = childOrganization1_1.Id,
                };
                consumptionObjectService.Add(consumptionObject1_1_1);
                consumptionObjectService.Add(consumptionObject1_1_2);
                
                // объекты потребления главной организации 1 - дочерней организации 2
                var consumptionObject1_2_1 = new ConsumptionObject
                {
                    Name = "ТЦ Энерго Москва-Север 1",
                    Address = "Бутлерова 2_1" ,
                    ChildOrganizationId = childOrganization1_2.Id,
                };
                var consumptionObject1_2_2 = new ConsumptionObject
                {
                    Name = "ТЦ Энерго Москва-Север 2",
                    Address = "Бутлерова 2_2" ,
                    ChildOrganizationId = childOrganization1_2.Id,
                };
                consumptionObjectService.Add(consumptionObject1_2_1);
                consumptionObjectService.Add(consumptionObject1_2_2);
                
                // У второй дочерней организации 2 объекта потребления
                // объекты потребления главной организации 2 - дочерней организации 1
                var consumptionObject2_1_1 = new ConsumptionObject
                {
                    Name = "ТЦ Энерго Новороссийск-Юг 1",
                    Address = "Полярная 1_1" ,
                    ChildOrganizationId = childOrganization2_1.Id,
                };
                var consumptionObject2_1_2 = new ConsumptionObject
                {
                    Name = "ТЦ Энерго Новороссийск-Юг 2",
                    Address = "Полярная 1_2" ,
                    ChildOrganizationId = childOrganization2_1.Id,
                };
                consumptionObjectService.Add(consumptionObject2_1_1);
                consumptionObjectService.Add(consumptionObject2_1_2);
                
                // объекты потребления главной организации 2 - дочерней организации 2
                var consumptionObject2_2_1 = new ConsumptionObject
                {
                    Name = "ТЦ Энерго Новороссийск-Север 1",
                    Address = "Полярная 2_1" ,
                    ChildOrganizationId = childOrganization2_2.Id,
                };
                var consumptionObject2_2_2 = new ConsumptionObject
                {
                    Name = "ТЦ Энерго Новороссийск-Север 2",
                    Address = "Полярная 2_2" ,
                    ChildOrganizationId = childOrganization2_2.Id,
                };
                consumptionObjectService.Add(consumptionObject2_2_1);
                consumptionObjectService.Add(consumptionObject2_2_2);
                
                // ---------------------------------------------------------------------------------------------------------
                // Для каждого объекта потребления нужно сделать свои точки измерения электроэнергии
                
                // Для 1-ого объекта потребления consumptionObject1_1_1 сделаем 2 точки измерения
                var measurementPoint1_1_1_1 = new MeasurementPoint
                {
                    Name = "measurementPoint1_1",
                    ConsumptionObjectId = consumptionObject1_1_1.Id,
                };
                var measurementPoint1_1_1_2 = new MeasurementPoint
                {
                    Name = "measurementPoint1_2",
                    ConsumptionObjectId = consumptionObject1_1_1.Id,
                };
                measurementPointService.Add(measurementPoint1_1_1_1);
                measurementPointService.Add(measurementPoint1_1_1_2);
                
                // Для 2-ого объекта потребления consumptionObject1_1_2 сделаем 2 точки измерения
                var measurementPoint1_1_2_1 = new MeasurementPoint
                {
                    Name = "measurementPoint2_1",
                    ConsumptionObjectId = consumptionObject1_1_2.Id,
                };
                var measurementPoint1_1_2_2 = new MeasurementPoint
                {
                    Name = "measurementPoint2_2",
                    ConsumptionObjectId = consumptionObject1_1_2.Id,
                };
                measurementPointService.Add(measurementPoint1_1_2_1);
                measurementPointService.Add(measurementPoint1_1_2_2);
                
                // Для 3-ого объекта потребления consumptionObject1_2_1 сделаем 2 точки измерения
                var measurementPoint1_2_1_1 = new MeasurementPoint
                {
                    Name = "measurementPoint3_1",
                    ConsumptionObjectId = consumptionObject1_2_1.Id,
                };
                var measurementPoint1_2_1_2 = new MeasurementPoint
                {
                    Name = "measurementPoint3_2",
                    ConsumptionObjectId = consumptionObject1_2_1.Id,
                };
                measurementPointService.Add(measurementPoint1_2_1_1);
                measurementPointService.Add(measurementPoint1_2_1_2);
                
                // Для 4-ого объекта потребления consumptionObject1_2_2 сделаем 2 точки измерения
                var measurementPoint1_2_2_1 = new MeasurementPoint
                {
                    Name = "measurementPoint4_1",
                    ConsumptionObjectId = consumptionObject1_2_2.Id,
                };
                var measurementPoint1_2_2_2 = new MeasurementPoint
                {
                    Name = "measurementPoint4_2",
                    ConsumptionObjectId = consumptionObject1_2_2.Id,
                };
                measurementPointService.Add(measurementPoint1_2_2_1);
                measurementPointService.Add(measurementPoint1_2_2_2);
                
                // Для 5-ого объекта потребления consumptionObject2_1_1 сделаем 2 точки измерения
                var measurementPoint2_1_1_1 = new MeasurementPoint
                {
                    Name = "measurementPoint5_1",
                    ConsumptionObjectId = consumptionObject2_1_1.Id,
                };
                var measurementPoint2_1_1_2 = new MeasurementPoint
                {
                    Name = "measurementPoint5_2",
                    ConsumptionObjectId = consumptionObject2_1_1.Id,
                };
                measurementPointService.Add(measurementPoint2_1_1_1);
                measurementPointService.Add(measurementPoint2_1_1_2);
                
                // Для 6-ого объекта потребления consumptionObject2_1_2 сделаем 2 точки измерения
                var measurementPoint2_1_2_1 = new MeasurementPoint
                {
                    Name = "measurementPoint6_1",
                    ConsumptionObjectId = consumptionObject2_1_2.Id,
                };
                var measurementPoint2_1_2_2 = new MeasurementPoint
                {
                    Name = "measurementPoint6_2",
                    ConsumptionObjectId = consumptionObject2_1_2.Id,
                };
                measurementPointService.Add(measurementPoint2_1_2_1);
                measurementPointService.Add(measurementPoint2_1_2_2);
                
                // Для 7-ого объекта потребления consumptionObject2_2_1 сделаем 2 точки измерения
                var measurementPoint2_2_1_1 = new MeasurementPoint
                {
                    Name = "measurementPoint7_1",
                    ConsumptionObjectId = consumptionObject2_2_1.Id,
                };
                var measurementPoint2_2_1_2 = new MeasurementPoint
                {
                    Name = "measurementPoint7_2",
                    ConsumptionObjectId = consumptionObject2_2_1.Id,
                };
                measurementPointService.Add(measurementPoint2_2_1_1);
                measurementPointService.Add(measurementPoint2_2_1_2);
                
                // Для 8-ого объекта потребления consumptionObject2_2_2 сделаем 2 точки измерения
                var measurementPoint2_2_2_1 = new MeasurementPoint
                {
                    Name = "measurementPoint8_1",
                    ConsumptionObjectId = consumptionObject2_2_2.Id,
                };
                var measurementPoint2_2_2_2 = new MeasurementPoint
                {
                    Name = "measurementPoint8_2",
                    ConsumptionObjectId = consumptionObject2_2_2.Id,
                };
                measurementPointService.Add(measurementPoint2_2_2_1);
                measurementPointService.Add(measurementPoint2_2_2_2);
                
                
                // ---------------------------------------------------------------------------------------------------------
                // Для каждого объекта потребления нужно сделать свои точки поставки электроэнергии
                
                // Для 1-ого объекта потребления consumptionObject1_1_1 сделаем 2 точки поставки
                var supplyPoint1_1_1_1 = new SupplyPoint
                {
                    Name = "supplyPoint1_1",
                    MaxPower = 600,
                    ConsumptionObjectId = consumptionObject1_1_1.Id,
                };
                var supplyPoint1_1_1_2 = new SupplyPoint
                {
                    Name = "supplyPoint1_2",
                    MaxPower = 800,
                    ConsumptionObjectId = consumptionObject1_1_1.Id,
                };
                supplyPointService.Add(supplyPoint1_1_1_1);
                supplyPointService.Add(supplyPoint1_1_1_2);
                
                // Для 2-ого объекта потребления consumptionObject1_1_2 сделаем 2 точки поставки
                var supplyPoint1_1_2_1 = new SupplyPoint
                {
                    Name = "supplyPoint2_1",
                    MaxPower = 800,
                    ConsumptionObjectId = consumptionObject1_1_2.Id,
                };
                var supplyPoint1_1_2_2 = new SupplyPoint
                {
                    Name = "supplyPoint2_2",
                    MaxPower = 600,
                    ConsumptionObjectId = consumptionObject1_1_2.Id,
                };
                supplyPointService.Add(supplyPoint1_1_2_1);
                supplyPointService.Add(supplyPoint1_1_2_2);
                
                // Для 3-ого объекта потребления consumptionObject1_2_1 сделаем 2 точки поставки
                var supplyPoint1_2_1_1 = new SupplyPoint
                {
                    Name = "supplyPoint3_1",
                    MaxPower = 900,
                    ConsumptionObjectId = consumptionObject1_2_1.Id,
                };
                var supplyPoint1_2_1_2 = new SupplyPoint
                {
                    Name = "supplyPoint3_2",
                    MaxPower = 1800,
                    ConsumptionObjectId = consumptionObject1_2_1.Id,
                };
                supplyPointService.Add(supplyPoint1_2_1_1);
                supplyPointService.Add(supplyPoint1_2_1_2);
                
                // Для 4-ого объекта потребления consumptionObject1_2_2 сделаем 2 точки поставки
                var supplyPoint1_2_2_1 = new SupplyPoint
                {
                    Name = "supplyPoint4_1",
                    MaxPower = 1200,
                    ConsumptionObjectId = consumptionObject1_2_2.Id,
                };
                var supplyPoint1_2_2_2 = new SupplyPoint
                {
                    Name = "supplyPoint4_2",
                    MaxPower = 800,
                    ConsumptionObjectId = consumptionObject1_2_2.Id,
                };
                supplyPointService.Add(supplyPoint1_2_2_1);
                supplyPointService.Add(supplyPoint1_2_2_2);
                
                // Для 5-ого объекта потребления consumptionObject2_1_1 сделаем 2 точки поставки
                var supplyPoint2_1_1_1 = new SupplyPoint
                {
                    Name = "supplyPoint5_1",
                    MaxPower = 1200,
                    ConsumptionObjectId = consumptionObject2_1_1.Id,
                };
                var supplyPoint2_1_1_2 = new SupplyPoint
                {
                    Name = "supplyPoint5_2",
                    MaxPower = 500,
                    ConsumptionObjectId = consumptionObject2_1_1.Id,
                };
                supplyPointService.Add(supplyPoint2_1_1_1);
                supplyPointService.Add(supplyPoint2_1_1_2);
                
                // Для 6-ого объекта потребления consumptionObject2_1_2 сделаем 2 точки поставки
                var supplyPoint2_1_2_1 = new SupplyPoint
                {
                    Name = "supplyPoint6_1",
                    MaxPower = 300,
                    ConsumptionObjectId = consumptionObject2_1_2.Id,
                };
                var supplyPoint2_1_2_2 = new SupplyPoint
                {
                    Name = "supplyPoint6_2",
                    MaxPower = 1000,
                    ConsumptionObjectId = consumptionObject2_1_2.Id,
                };
                supplyPointService.Add(supplyPoint2_1_2_1);
                supplyPointService.Add(supplyPoint2_1_2_2);
                
                // Для 7-ого объекта потребления consumptionObject2_2_1 сделаем 2 точки поставки
                var supplyPoint2_2_1_1 = new SupplyPoint
                {
                    Name = "supplyPoint7_1",
                    MaxPower = 1400,
                    ConsumptionObjectId = consumptionObject2_2_1.Id,
                };
                var supplyPoint2_2_1_2 = new SupplyPoint
                {
                    Name = "supplyPoint7_2",
                    MaxPower = 850,
                    ConsumptionObjectId = consumptionObject2_2_1.Id,
                };
                supplyPointService.Add(supplyPoint2_2_1_1);
                supplyPointService.Add(supplyPoint2_2_1_2);
                
                // Для 8-ого объекта потребления consumptionObject2_2_2 сделаем 2 точки поставки
                var supplyPoint2_2_2_1 = new SupplyPoint
                {
                    Name = "supplyPoint8_1",
                    MaxPower = 900,
                    ConsumptionObjectId = consumptionObject2_2_2.Id,
                };
                var supplyPoint2_2_2_2 = new SupplyPoint
                {
                    Name = "supplyPoint8_2",
                    MaxPower = 800,
                    ConsumptionObjectId = consumptionObject2_2_2.Id,
                };
                supplyPointService.Add(supplyPoint2_2_2_1);
                supplyPointService.Add(supplyPoint2_2_2_2);
                
                // ---------------------------------------------------------------------------------------------------------
                // Для каждой точки измерения (1..16) нужно сделать по одному счетчику, транформатору тока, напряжения
                
                // Для 1-ой точки измерения measurementPoint1_1_1_1
                var electricityMeterPoint1_1_1_1 = new ElectricityMeter
                {
                    Number = "1_1_1_1",
                    Type = "Electricity Meter",
                    VerificationDate = new DateTime(2020, 1, 31),
                    MeasurementPointId = measurementPoint1_1_1_1.Id
                };
                var voltageTransformerPoint1_1_1_1 = new VoltageTransformer
                {
                    Number = "1_1_1_1",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2020, 1, 31),
                    MeasurementPointId = measurementPoint1_1_1_1.Id
                };
                var currentTransformerPoint1_1_1_1 = new CurrentTransformer
                {
                    Number = "1_1_1_1",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2020, 1, 31),
                    MeasurementPointId = measurementPoint1_1_1_1.Id
                };
                electricityMeterService.Add(electricityMeterPoint1_1_1_1);
                currentTransformerService.Add(currentTransformerPoint1_1_1_1);
                voltageTransformerService.Add(voltageTransformerPoint1_1_1_1);
                
                // Для 2-ой точки измерения measurementPoint1_1_1_2
                var electricityMeterPoint1_1_1_2 = new ElectricityMeter
                {
                    Number = "1_1_1_2",
                    Type = "Electricity Meter",
                    VerificationDate = new DateTime(2018, 1, 31),
                    MeasurementPointId = measurementPoint1_1_1_2.Id
                };
                var voltageTransformerPoint1_1_1_2 = new VoltageTransformer
                {
                    Number = "1_1_1_2",
                    Type = "ТЛ",
                    TransformationRatio = 12,
                    VerificationDate = new DateTime(2018, 1, 31),
                    MeasurementPointId = measurementPoint1_1_1_2.Id
                };
                var currentTransformerPoint1_1_1_2 = new CurrentTransformer
                {
                    Number = "1_1_1_2",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2018, 1, 31),
                    MeasurementPointId = measurementPoint1_1_1_2.Id
                };
                electricityMeterService.Add(electricityMeterPoint1_1_1_2);
                currentTransformerService.Add(currentTransformerPoint1_1_1_2);
                voltageTransformerService.Add(voltageTransformerPoint1_1_1_2);
                
                // Для 3-ей точки измерения measurementPoint1_1_2_1
                var electricityMeterPoint1_1_2_1 = new ElectricityMeter
                {
                    Number = "1_1_2_1",
                    Type = "Electricity Meter",
                    VerificationDate = new DateTime(2017, 1, 31),
                    MeasurementPointId = measurementPoint1_1_2_1.Id
                };
                var voltageTransformerPoint1_1_2_1 = new VoltageTransformer
                {
                    Number = "1_1_2_1",
                    Type = "ТЛ",
                    TransformationRatio = 12,
                    VerificationDate = new DateTime(2017, 1, 31),
                    MeasurementPointId = measurementPoint1_1_2_1.Id
                };
                var currentTransformerPoint1_1_2_1 = new CurrentTransformer
                {
                    Number = "1_1_2_1",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2017, 1, 31),
                    MeasurementPointId = measurementPoint1_1_2_1.Id
                };
                electricityMeterService.Add(electricityMeterPoint1_1_2_1);
                currentTransformerService.Add(currentTransformerPoint1_1_2_1);
                voltageTransformerService.Add(voltageTransformerPoint1_1_2_1);
                
                // Для 4-ой точки измерения measurementPoint1_1_2_2
                var electricityMeterPoint1_1_2_2 = new ElectricityMeter
                {
                    Number = "1_1_2_2",
                    Type = "Electricity Meter",
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint1_1_2_2.Id
                };
                var voltageTransformerPoint1_1_2_2 = new VoltageTransformer
                {
                    Number = "1_1_2_2",
                    Type = "ТЛ",
                    TransformationRatio = 12,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint1_1_2_2.Id
                };
                var currentTransformerPoint1_1_2_2 = new CurrentTransformer
                {
                    Number = "1_1_2_2",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint1_1_2_2.Id
                };
                electricityMeterService.Add(electricityMeterPoint1_1_2_2);
                currentTransformerService.Add(currentTransformerPoint1_1_2_2);
                voltageTransformerService.Add(voltageTransformerPoint1_1_2_2);
                
                // Для 5-ой точки измерения measurementPoint1_2_1_1
                var electricityMeterPoint1_2_1_1 = new ElectricityMeter
                {
                    Number = "1_2_1_1",
                    Type = "Electricity Meter",
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint1_2_1_1.Id
                };
                var voltageTransformerPoint1_2_1_1 = new VoltageTransformer
                {
                    Number = "1_2_1_1",
                    Type = "ТЛ",
                    TransformationRatio = 12,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint1_2_1_1.Id
                };
                var currentTransformerPoint1_2_1_1 = new CurrentTransformer
                {
                    Number = "1_2_1_1",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint1_2_1_1.Id
                };
                electricityMeterService.Add(electricityMeterPoint1_2_1_1);
                currentTransformerService.Add(currentTransformerPoint1_2_1_1);
                voltageTransformerService.Add(voltageTransformerPoint1_2_1_1);
                
                // Для 6-ой точки измерения measurementPoint1_2_1_2
                var electricityMeterPoint1_2_1_2 = new ElectricityMeter
                {
                    Number = "1_2_1_2",
                    Type = "Electricity Meter",
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint1_2_1_2.Id
                };
                var voltageTransformerPoint1_2_1_2 = new VoltageTransformer
                {
                    Number = "1_2_1_2",
                    Type = "ТЛ",
                    TransformationRatio = 12,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint1_2_1_2.Id
                };
                var currentTransformerPoint1_2_1_2 = new CurrentTransformer
                {
                    Number = "1_2_1_2",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint1_2_1_2.Id
                };
                electricityMeterService.Add(electricityMeterPoint1_2_1_2);
                currentTransformerService.Add(currentTransformerPoint1_2_1_2);
                voltageTransformerService.Add(voltageTransformerPoint1_2_1_2);
                
                // Для 7-ой точки измерения measurementPoint1_2_2_1
                var electricityMeterPoint1_2_2_1 = new ElectricityMeter
                {
                    Number = "1_2_2_1",
                    Type = "Electricity Meter",
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint1_2_2_1.Id
                };
                var voltageTransformerPoint1_2_2_1 = new VoltageTransformer
                {
                    Number = "1_2_2_1",
                    Type = "ТЛ",
                    TransformationRatio = 12,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint1_2_2_1.Id
                };
                var currentTransformerPoint1_2_2_1 = new CurrentTransformer
                {
                    Number = "1_2_2_1",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint1_2_2_1.Id
                };
                electricityMeterService.Add(electricityMeterPoint1_2_2_1);
                currentTransformerService.Add(currentTransformerPoint1_2_2_1);
                voltageTransformerService.Add(voltageTransformerPoint1_2_2_1);
                
                // Для 8-ой точки измерения measurementPoint1_2_2_2
                var electricityMeterPoint1_2_2_2 = new ElectricityMeter
                {
                    Number = "1_2_2_2",
                    Type = "Electricity Meter",
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint1_2_2_2.Id
                };
                var voltageTransformerPoint1_2_2_2 = new VoltageTransformer
                {
                    Number = "1_2_2_2",
                    Type = "ТЛ",
                    TransformationRatio = 12,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint1_2_2_2.Id
                };
                var currentTransformerPoint1_2_2_2 = new CurrentTransformer
                {
                    Number = "1_2_2_2",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint1_2_2_2.Id
                };
                electricityMeterService.Add(electricityMeterPoint1_2_2_2);
                currentTransformerService.Add(currentTransformerPoint1_2_2_2);
                voltageTransformerService.Add(voltageTransformerPoint1_2_2_2);
                
                
                // Для 9-ой точки измерения measurementPoint2_1_1_1
                var electricityMeterPoint2_1_1_1 = new ElectricityMeter
                {
                    Number = "2_1_1_1",
                    Type = "Electricity Meter",
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_1_1_1.Id
                };
                var voltageTransformerPoint2_1_1_1 = new VoltageTransformer
                {
                    Number = "2_1_1_1",
                    Type = "ТЛ",
                    TransformationRatio = 12,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_1_1_1.Id
                };
                var currentTransformerPoint2_1_1_1 = new CurrentTransformer
                {
                    Number = "2_1_1_1",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_1_1_1.Id
                };
                electricityMeterService.Add(electricityMeterPoint2_1_1_1);
                currentTransformerService.Add(currentTransformerPoint2_1_1_1);
                voltageTransformerService.Add(voltageTransformerPoint2_1_1_1);
                
                // Для 10-ой точки измерения measurementPoint2_1_1_2
                var electricityMeterPoint2_1_1_2 = new ElectricityMeter
                {
                    Number = "2_1_1_2",
                    Type = "Electricity Meter",
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_1_1_2.Id
                };
                var voltageTransformerPoint2_1_1_2 = new VoltageTransformer
                {
                    Number = "2_1_1_2",
                    Type = "ТЛ",
                    TransformationRatio = 12,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_1_1_2.Id
                };
                var currentTransformerPoint2_1_1_2 = new CurrentTransformer
                {
                    Number = "2_1_1_2",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_1_1_2.Id
                };
                electricityMeterService.Add(electricityMeterPoint2_1_1_2);
                currentTransformerService.Add(currentTransformerPoint2_1_1_2);
                voltageTransformerService.Add(voltageTransformerPoint2_1_1_2);
                
                // Для 11-ой точки измерения measurementPoint2_1_2_1
                var electricityMeterPoint2_1_2_1 = new ElectricityMeter
                {
                    Number = "2_1_2_1",
                    Type = "Electricity Meter",
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_1_2_1.Id
                };
                var voltageTransformerPoint2_1_2_1 = new VoltageTransformer
                {
                    Number = "2_1_2_1",
                    Type = "ТЛ",
                    TransformationRatio = 12,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_1_2_1.Id
                };
                var currentTransformerPoint2_1_2_1 = new CurrentTransformer
                {
                    Number = "2_1_2_1",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_1_2_1.Id
                };
                electricityMeterService.Add(electricityMeterPoint2_1_2_1);
                currentTransformerService.Add(currentTransformerPoint2_1_2_1);
                voltageTransformerService.Add(voltageTransformerPoint2_1_2_1);
                
                // Для 12-ой точки измерения measurementPoint2_1_2_2
                var electricityMeterPoint2_1_2_2 = new ElectricityMeter
                {
                    Number = "2_1_2_2",
                    Type = "Electricity Meter",
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_1_2_2.Id
                };
                var voltageTransformerPoint2_1_2_2 = new VoltageTransformer
                {
                    Number = "2_1_2_2",
                    Type = "ТЛ",
                    TransformationRatio = 12,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_1_2_2.Id
                };
                var currentTransformerPoint2_1_2_2 = new CurrentTransformer
                {
                    Number = "2_1_2_2",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_1_2_2.Id
                };
                electricityMeterService.Add(electricityMeterPoint2_1_2_2);
                currentTransformerService.Add(currentTransformerPoint2_1_2_2);
                voltageTransformerService.Add(voltageTransformerPoint2_1_2_2);
                
                // Для 13-ой точки измерения measurementPoint2_2_1_1
                var electricityMeterPoint2_2_1_1 = new ElectricityMeter
                {
                    Number = "2_2_1_1",
                    Type = "Electricity Meter",
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_2_1_1.Id
                };
                var voltageTransformerPoint2_2_1_1 = new VoltageTransformer
                {
                    Number = "2_2_1_1",
                    Type = "ТЛ",
                    TransformationRatio = 12,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_2_1_1.Id
                };
                var currentTransformerPoint2_2_1_1 = new CurrentTransformer
                {
                    Number = "2_2_1_1",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_2_1_1.Id
                };
                electricityMeterService.Add(electricityMeterPoint2_2_1_1);
                currentTransformerService.Add(currentTransformerPoint2_2_1_1);
                voltageTransformerService.Add(voltageTransformerPoint2_2_1_1);
                
                // Для 14-ой точки измерения measurementPoint2_2_1_2
                var electricityMeterPoint2_2_1_2 = new ElectricityMeter
                {
                    Number = "2_2_1_2",
                    Type = "Electricity Meter",
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_2_1_2.Id
                };
                var voltageTransformerPoint2_2_1_2 = new VoltageTransformer
                {
                    Number = "2_2_1_2",
                    Type = "ТЛ",
                    TransformationRatio = 12,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_2_1_2.Id
                };
                var currentTransformerPoint2_2_1_2 = new CurrentTransformer
                {
                    Number = "2_2_1_2",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_2_1_2.Id
                };
                electricityMeterService.Add(electricityMeterPoint2_2_1_2);
                currentTransformerService.Add(currentTransformerPoint2_2_1_2);
                voltageTransformerService.Add(voltageTransformerPoint2_2_1_2);
                
                // Для 15-ой точки измерения measurementPoint2_2_2_1
                var electricityMeterPoint2_2_2_1 = new ElectricityMeter
                {
                    Number = "2_2_2_1",
                    Type = "Electricity Meter",
                    VerificationDate = new DateTime(2024, 3, 31),
                    MeasurementPointId = measurementPoint2_2_2_1.Id
                };
                var voltageTransformerPoint2_2_2_1 = new VoltageTransformer
                {
                    Number = "2_2_2_1",
                    Type = "ТЛ",
                    TransformationRatio = 12,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_2_2_1.Id
                };
                var currentTransformerPoint2_2_2_1 = new CurrentTransformer
                {
                    Number = "2_2_2_1",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_2_2_1.Id
                };
                electricityMeterService.Add(electricityMeterPoint2_2_2_1);
                currentTransformerService.Add(currentTransformerPoint2_2_2_1);
                voltageTransformerService.Add(voltageTransformerPoint2_2_2_1);
                
                // Для 16-ой точки измерения measurementPoint2_2_2_2
                var electricityMeterPoint2_2_2_2 = new ElectricityMeter
                {
                    Number = "2_2_2_2",
                    Type = "Electricity Meter",
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_2_2_2.Id
                };
                var voltageTransformerPoint2_2_2_2 = new VoltageTransformer
                {
                    Number = "2_2_2_2",
                    Type = "ТЛ",
                    TransformationRatio = 12,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_2_2_2.Id
                };
                var currentTransformerPoint2_2_2_2 = new CurrentTransformer
                {
                    Number = "2_2_2_2",
                    Type = "ТПЛ",
                    TransformationRatio = 15,
                    VerificationDate = new DateTime(2019, 1, 31),
                    MeasurementPointId = measurementPoint2_2_2_2.Id
                };
                electricityMeterService.Add(electricityMeterPoint2_2_2_2);
                currentTransformerService.Add(currentTransformerPoint2_2_2_2);
                voltageTransformerService.Add(voltageTransformerPoint2_2_2_2);
                
                
                
                // ---------------------------------------------------------------------------------------------------------
                
                
                // Для каждого объета потребления может быть несколько точек поставки электроэнергии
                // и несколько точек измерения
                
                // Для первого объекта потребления
                // 1-ый расчетный прибор учета (на разные точки измерения в разные периоды времени приходит электричество
                // с одной точки поставки
                var сalculationMeter1 = new CalculationMeter
                {
                    MeasurementPointId = measurementPoint1_1_1_1.Id,
                    SupplyPointId = supplyPoint1_1_1_1.Id,
                    StartTime = new DateTime(2019, 1, 31),
                    EndTime = new DateTime(2024, 1, 31),
                };
                calculationMeterService.Add(сalculationMeter1);
                
                // 2-ый расчетный прибор учета
                var сalculationMeter2 = new CalculationMeter
                {
                    MeasurementPointId = measurementPoint1_1_1_2.Id,
                    SupplyPointId = supplyPoint1_1_1_1.Id,
                    StartTime = new DateTime(2016, 1, 31),
                    EndTime = new DateTime(2019, 1, 31),
                };
                calculationMeterService.Add(сalculationMeter2);
                
                // Для второго объекта потребления
                // на одну и ту же точку измерения в разные периоды времени приходит электричество
                // с одной точки поставки
                
                // 3-ый расчетный прибор учета
                var сalculationMeter3 = new CalculationMeter
                {
                    MeasurementPointId = measurementPoint1_1_2_1.Id,
                    SupplyPointId = supplyPoint1_1_2_1.Id,
                    StartTime = new DateTime(2011, 1, 31),
                    EndTime = new DateTime(2025, 1, 31),
                };
                calculationMeterService.Add(сalculationMeter3);
                
                // 4-ый расчетный прибор учета
                var сalculationMeter4 = new CalculationMeter
                {
                    MeasurementPointId = measurementPoint1_1_2_1.Id,
                    SupplyPointId = supplyPoint1_1_2_1.Id,
                    StartTime = new DateTime(2015, 1, 31),
                    EndTime = new DateTime(2025, 1, 31),
                };
                calculationMeterService.Add(сalculationMeter4);
                
                // Для третьего объекта потребления
                // на разные точку измерения в разные периоды времени приходит электричество
                // с разных точек поставки
                
                // 5-ый расчетный прибор учета
                var сalculationMeter5 = new CalculationMeter
                {
                    MeasurementPointId = measurementPoint1_2_1_1.Id,
                    SupplyPointId = supplyPoint1_2_1_1.Id,
                    StartTime = new DateTime(2020, 1, 31),
                    EndTime = new DateTime(2025, 1, 31),
                };
                calculationMeterService.Add(сalculationMeter5);
                
                // 6-ый расчетный прибор учета
                var сalculationMeter6 = new CalculationMeter
                {
                    MeasurementPointId = measurementPoint1_2_1_2.Id,
                    SupplyPointId = supplyPoint1_2_1_2.Id,
                    StartTime = new DateTime(2021, 1, 31),
                    EndTime = new DateTime(2026, 1, 31),
                };
                calculationMeterService.Add(сalculationMeter6);
                
                Console.WriteLine("Database initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while initializing the database: {ex.Message}");
            }
        }
    }
}

