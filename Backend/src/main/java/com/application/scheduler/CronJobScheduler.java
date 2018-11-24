package com.application.scheduler;

import com.application.repositories.UserLogsRepository;
import com.application.repositories.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Service;

import java.util.logging.Logger;

@Service
public class CronJobScheduler {

    public static final Logger log = Logger.getLogger(CronJobScheduler.class.getName());

    @Autowired
    private UserLogsRepository userLogsRepository;

    @Autowired
    private UserRepository userRepository;

    @Scheduled(cron = "*/30 * * * * *")
    public void updateSpeedForAllUsers() {
        log.info("Updating speed for all users");
    }

    @Scheduled(cron = "*/30 * * * * *")
    public void updateLevelOfExpertiseForAllUsers() {
        log.info("Updating level of expertise for all users");
    }

}
