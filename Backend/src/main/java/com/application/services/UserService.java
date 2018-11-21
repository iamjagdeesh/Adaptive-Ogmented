package com.application.services;

import com.application.pojo.User;
import com.application.pojo.UserLogs;
import com.application.repositories.UserLogsRepository;
import com.application.repositories.UserRepository;
import org.json.JSONObject;
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

    public JSONObject getGameSettingsForUser(String userId) {
        User user = userRepository.findByUserId(userId);

        JSONObject result = new JSONObject();
        result.put("speed", user.getSpeed());
        result.put("taskNumber", getNextTaskNumberForUser(userId));

        return result;
    }

    private Integer getNextTaskNumberForUser(String userId) {
        UserLogs latestSuccessfulLog = userLogsRepository.findTop1ByUserIdAndIsSuccessOrderByTimestampDesc(userId, "TRUE");
        if (latestSuccessfulLog == null) {
            return 1;
        }

        if (latestSuccessfulLog.getTaskNumber() == 4) {
            return 4;
        }

        return latestSuccessfulLog.getTaskNumber() + 1;

    }

    public JSONObject getEndOfSceneStats(String userId, Integer taskNumber) {
        JSONObject jsonObject = new JSONObject();

        jsonObject.put("averageGlobalSuccessfulCompletionTime", getAverageGlobalSuccessfulCompletionTime(taskNumber));
        jsonObject.put("averageUserSuccessfulCompletionTime", getAverageUserSuccessfulCompletionTime(taskNumber, userId));
        jsonObject.put("previousCompletionTime", getPreviousCompletionTime(taskNumber, userId));
        jsonObject.put("previousSuccessfulCompletionTime", getPreviousSuccessfulCompletionTime(taskNumber, userId));

        return jsonObject;
    }

    private Long getAverageGlobalSuccessfulCompletionTime(Integer taskNumber) {
        List<UserLogs> logs = userLogsRepository.findByTaskNumberAndIsSuccess(taskNumber, "TRUE");
        if(logs.size() == 0) {
        	return 0L;
        }
        Long sum = logs.stream().mapToLong(i -> i.getTimeTaken()).sum();

        return sum / logs.size();
    }

    private Long getAverageUserSuccessfulCompletionTime(Integer taskNumber, String userId) {
        List<UserLogs> logs = userLogsRepository.findByUserIdAndTaskNumberAndIsSuccess(userId, taskNumber, "TRUE");
        if(logs.size() == 0) {
        	return 0L;
        }
        Long sum = logs.stream().mapToLong(i -> i.getTimeTaken()).sum();

        return sum / logs.size();
    }

    private Long getPreviousCompletionTime(Integer taskNumber, String userId) {
        UserLogs log = userLogsRepository.findTop1ByUserIdAndTaskNumberOrderByTimestampDesc(userId, taskNumber);

        return log.getTimeTaken();
    }

    private Long getPreviousSuccessfulCompletionTime(Integer taskNumber, String userId) {
        UserLogs log = userLogsRepository.findTop1ByUserIdAndTaskNumberAndIsSuccessOrderByTimestampDesc(userId, taskNumber, "TRUE");

        return log.getTimeTaken();
    }

}
