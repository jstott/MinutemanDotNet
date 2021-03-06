﻿namespace Minuteman
{
    public class ActivitySettings
    {
        private const int DefaultDb = 0;
        private const ActivityDrilldown DefaultDrilDown =
            ActivityDrilldown.Hour;

        private const string DefaultKeyPrefix = "minuteman";
        private const string DefaultKeySeparator = ":";
 
        public ActivitySettings() : 
            this(
            DefaultDb,
            DefaultDrilDown,
            DefaultKeyPrefix,
            DefaultKeySeparator)
        {
        }

        public ActivitySettings(
            int db,
            ActivityDrilldown drilldown)
            : this(db, drilldown, DefaultKeyPrefix, DefaultKeySeparator)
        {
        }

        public ActivitySettings(int db)
            : this(
            db,
            DefaultDrilDown,
            DefaultKeyPrefix,
            DefaultKeySeparator)
        {
        }

        public ActivitySettings(ActivityDrilldown drilldown)
            : this(
            DefaultDb,
            drilldown,
            DefaultKeyPrefix,
            DefaultKeySeparator)
        {
        }

        public ActivitySettings(
            int db,
            ActivityDrilldown drilldown,
            string keyPrefix,
            string keySeparator)
        {
            Validation.ValidateDb(db);

            Db = db;
            Drilldown = drilldown;
            KeyPrefix = keyPrefix ?? string.Empty;
            KeySeparator = keySeparator ?? string.Empty;
        }

        public int Db { get; private set; }

        public ActivityDrilldown Drilldown { get; private set; }

        public string KeyPrefix { get; private set; }

        public string KeySeparator { get; private set; }
    }
}