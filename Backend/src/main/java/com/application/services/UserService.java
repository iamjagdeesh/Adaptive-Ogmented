package com.application.services;

import com.application.pojo.User;
import com.application.pojo.UserLogs;
import com.application.repositories.UserLogsRepository;
import com.application.repositories.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Date;
import java.util.List;

@Service
public class UserService {

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private UserLogsRepository userLogsRepository;

    public User addUser(String userId, String password, Integer levelOfExpertise) {
        User user = new User(userId, password, levelOfExpertise, 10);
        userRepository.save(user);

        return user;
    }

    public User getUser(String userId) {
        User user = userRepository.findByUserId(userId);

        return user;
    }

    public Boolean checkIfValidCredentials(String userId, String password) {
        User user = userRepository.findByUserIdAndPassword(userId, password);
        if (user == null) {
            return Boolean.FALSE;
        } else {
            return Boolean.TRUE;
        }
    }

    public UserLogs addUserLog(Integer speed, Integer numberOfWallCollisions, Integer taskNumber, String userId, Boolean isSuccess, Long timeTaken) {
        String stringIsSuccess = isSuccess ? "TRUE" : "FALSE";
        Date timestamp = new Date();

        UserLogs log = new UserLogs(speed, numberOfWallCollisions, taskNumber, userId, stringIsSuccess, timeTaken, timestamp);
        userLogsRepository.save(log);

        return log;
    }

    public List<UserLogs> getUserLogs(String userId) {
        List<UserLogs> logs = userLogsRepository.findByUserId(userId);

        return logs;
    }

}
