#!/bin/bash
echo '{
	"Data": {
    "DefaultConnection": {
      "ConnectionString": "User ID='$POSTGRES_ENV_POSTGRES_USER';Password='$POSTGRES_ENV_POSTGRES_PASSWORD';Host='$POSTGRES_PORT_5432_TCP_ADDR';Port='$POSTGRES_PORT_5432_TCP_PORT';Database='$POSTGRES_ENV_POSTGRES_DB';Pooling=true;"
    }
	}
}' > config.json