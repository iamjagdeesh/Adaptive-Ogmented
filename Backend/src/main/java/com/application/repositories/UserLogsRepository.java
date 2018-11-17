package com.application.repositories;

import com.application.pojo.UserLogs;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface UserLogsRepository extends JpaRepository<UserLogs, Integer> {

    List<UserLogs> findByUserId(String userId);

}
